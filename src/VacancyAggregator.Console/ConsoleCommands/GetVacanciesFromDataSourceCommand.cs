using Autofac;
using AutoMapper;
using Microsoft.Extensions.CommandLineUtils;
using NLog;
using VacancyAggregator.Core;
using VacancyAggregator.Data;
using VacancyAggregator.Domain.Interfaces;
using VacancyAggregator.Domain.Models;
using VacancyAggregator.VacancySources.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacancyFilter = VacancyAggregator.Domain.Models.VacancyFilter;
using Vacancy = VacancyAggregator.Domain.Models.Vacancy;

namespace VacancyAggregator.Console.ConsoleCommands
{
    internal class GetVacanciesFromDataSourceCommand
    {
        public static void Register(CommandLineApplication app)
        {
            app.Command("get-vacancies", Setup);
        }

        private static void Setup(CommandLineApplication command)
        {
            command.HelpOption("-?|-h|--help");
            command.Description = "Выгрузка вакансий из источника данных.";

            var dataSourceIdOption = command.Option(
                  "-d|--data-source",
                  "Идентификатор источника вакансий.",
                  CommandOptionType.SingleValue);

            var VacancyFilterIdOption = command.Option(
                   "-s|--search-filter-id",
                   "Идентификатор фильтра, по которым будут фильтроватся вакансии для импорта.",
                   CommandOptionType.SingleValue);

            var saveToDbOption = command.Option(
                   "-db|--save-to-db",
                   "Сохранить в базу данных Search Work. Флаг. Необязательный параметр.",
                   CommandOptionType.NoValue);

            command.ExecuteWithContainer((container) =>
            {
                var logger = container.Resolve<ILogger>();
                var unitOfWork = container.Resolve<IUnitOfWork>();
                var mapper = container.Resolve<IMapper>();
                var workService = container.Resolve<VacancySourceService>();

                logger.Info($"Выполнение команды {command.Description}.");

                if (!dataSourceIdOption.HasValue())
                {
                    throw new CommandParsingException(command, "Не указан идентификатора источника данных, из которого " +
                        "будет происходить импорт вакансий.");
                }

                List<VacancyFilter> VacancyFilters;

                if (!VacancyFilterIdOption.HasValue())
                {
                    logger.Warn("Не указан идетификатор фильтра. Будет произведена выгрузка вакансий по всем фильтрам.");

                    VacancyFilters = unitOfWork.VacancyFilter.GetAllFilters(false).ToList();
                }
                else
                {
                    VacancyFilters = new List<VacancyFilter>();

                    var VacancyFilterId = Convert.ToInt32(VacancyFilterIdOption.Value());
                    var entityVacancyFilter = unitOfWork.VacancyFilter.GetById(VacancyFilterId, false);

                    if (entityVacancyFilter == null)
                        throw new Exception($"VacancyFilter с Id: {VacancyFilterId} не найден в БД");

                    VacancyFilters.Add(entityVacancyFilter);
                }

                var dataSourceId = Convert.ToInt32(dataSourceIdOption.Value());
                DataSource dataSource = unitOfWork.DataSource.GetById(dataSourceId, false);

                List<Vacancy> vacancies = new List<Vacancy>();
                foreach (var VacancyFilter in VacancyFilters)
                {
                    vacancies.AddRange(workService.GetVacancies(dataSource, VacancyFilter));
                }

                logger.Info($"Выгружено {vacancies.Count} вакансий.");

                foreach(var vacancy in vacancies)
                {
                    logger.Info($"SourceId: {vacancy.DataSourceId}. Vacancy name: {vacancy.Name}");
                }

                if (saveToDbOption.HasValue())
                {
                    foreach(var vacancy in vacancies)
                    {
                        unitOfWork.Vacancy.AddOrUpdate(vacancy);
                    }

                    unitOfWork.Save();
                }

                logger.Info($"Выполнение команды {command.Description} успешно завершено.");
                return 0;
            });
        }
    }
}
