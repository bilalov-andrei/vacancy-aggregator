using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NLog;
using VacancyAggregator.Data;
using VacancyAggregator.Domain.Interfaces;
using VacancyAggregator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyAggregator.Console.ConsoleCommands
{
    internal class GetVacancyFiltersCommand
    {
        public static void Register(CommandLineApplication app)
        {
            app.Command("get-sf", Setup);
        }

        private static void Setup(CommandLineApplication command)
        {
            command.HelpOption("-?|-h|--help");
            command.Description = "Получить поисковые фильтры из базы данных VacancyAggregator";

            var filterIdOption = command.Option(
                 "-i|--filter-id",
                 "Идентификатор фильтра",
                 CommandOptionType.SingleValue);

            command.ExecuteWithContainer((container) =>
            {
                var logger = container.Resolve<ILogger>();
                var dbContext = container.Resolve<AppDbContext>();
                var unitOfWork = container.Resolve<IUnitOfWork>();

                var VacancyFilters = new List<VacancyFilter>();

                logger.Info($"Выполнение команды {command.Description}.");

                if (filterIdOption.HasValue())
                {
                    var filterId = Convert.ToInt32(filterIdOption.Value());
                    var VacancyFilter = unitOfWork.VacancyFilter.GetById(filterId, false);

                    if (VacancyFilter == null)
                        throw new Exception("Не найден фильтр с id" + filterId);

                    VacancyFilters.Add(VacancyFilter);
                }
                else
                {
                    VacancyFilters.AddRange(dbContext.VacancyFilters.ToList());
                }

                foreach(var filter in VacancyFilters)
                {
                    logger.Info($"Id: {filter.Id} Text: {filter.Text} Experience: {filter.Experience} Area: {filter.Area}");
                }

                logger.Info($"Выполнение команды {command.Description} завершено.");
                return 0;
            });
        }
    }
}
