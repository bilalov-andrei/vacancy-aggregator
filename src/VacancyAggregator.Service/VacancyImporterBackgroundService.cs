using Cronos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NLog;
using VacancyAggregator.Core;
using VacancyAggregator.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VacancyAggregator.Service
{
    public class VacancyImporterBackgroundService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly VacancySourceService _vacancySourceService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostApplicationLifetime _app;

        public VacancyImporterBackgroundService(IHostApplicationLifetime app, ILogger logger, VacancySourceService service, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _app = app;
            _logger = logger;
            _vacancySourceService = service;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.Info("Worker running at: {time}", DateTimeOffset.Now);

                    var dataSources = _unitOfWork.DataSource.FindByCondition(x => x.IsEnabled && x.IsExternal, false).ToList();
                    var vacancyFilters = await _unitOfWork.VacancyFilter.GetAllFiltersAsync(false);

                    foreach (var dataSource in dataSources)
                    {
                        foreach (var vacancyFilter in vacancyFilters)
                        {
                            _logger.Info($"Импорт вакансий из источника данных Name: {dataSource.Name}, Id: {dataSource.Id} с фильтром {vacancyFilter.Id}");

                            var vacancies = _vacancySourceService.GetVacancies(dataSource, vacancyFilter);

                            var data = JsonConvert.SerializeObject(vacancies);
                            _logger.Info(data);

                            foreach (var vacancy in vacancies)
                            {
                                _unitOfWork.Vacancy.AddOrUpdate(vacancy);
                                _unitOfWork.Save();
                            }

                            _logger.Info($"Импорт вакансий из источника данных Name: {dataSource.Name}, Id: {dataSource.Id} с фильтром {vacancyFilter.Id} закончен");
                        }
                    }

                    //TODO better to move launching service on cron level, but then not easy run through docker
                    var exetuceTime = _configuration["VacancyImporterBackgroundService:DateTime"];
                    await WaitForNextSchedule(exetuceTime, stoppingToken);
                }
            }
            catch (OperationCanceledException ex)
            {
                _logger.Warn(ex, "Process was canceled");
                Environment.ExitCode = -1;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Process caught exception");
                Environment.ExitCode = -1;
            }
            finally
            {
                _app.StopApplication();
            }
        }

        private async Task WaitForNextSchedule(string cronExpression, CancellationToken stoppingToken)
        {
            var parsedExp = CronExpression.Parse(cronExpression);
            var currentUtcTime = DateTimeOffset.UtcNow.UtcDateTime;
            var occurenceTime = parsedExp.GetNextOccurrence(currentUtcTime);

            var delay = occurenceTime.GetValueOrDefault() - currentUtcTime;
            _logger.Info("The run is delayed for {delay}. Current time: {time}", delay, DateTimeOffset.Now);

            await Task.Delay(delay, stoppingToken);
        }
    }
}
