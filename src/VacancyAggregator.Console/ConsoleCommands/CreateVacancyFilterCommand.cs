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
    internal class CreateVacancyFilterCommand
    {
        public static void Register(CommandLineApplication app)
        {
            app.Command("create-vacancy-filter", Setup);
        }

        private static void Setup(CommandLineApplication command)
        {
            command.HelpOption("-?|-h|--help");
            command.Description = "Добавление фильтра вакансий в базу данных VacancyAggregator";

            var textOption = command.Option(
                 "-t|--text",
                 "Ключевые слова для фильтра",
                 CommandOptionType.SingleValue);

            var experienceOption = command.Option(
                   "-e|--experience",
                   "Требуемый опыт для фильтра.",
                   CommandOptionType.SingleValue);


            command.ExecuteWithContainer((container) =>
            {
                var logger = container.Resolve<ILogger>();

                logger.Info($"Выполнение команды {command.Description}.");

                if (!textOption.HasValue())
                {
                    throw new CommandParsingException(command, "Не указаны ключевые слова для фильтра.");
                }

                if (!experienceOption.HasValue())
                {
                    throw new CommandParsingException(command, "Не указан требуемый опыт для фильтра.");
                }

                var unitOfWork = container.Resolve<IUnitOfWork>();

                var text = textOption.Value();
                var experience = experienceOption.Value();

                VacancyFilter VacancyFilter = new VacancyFilter()
                {
                    Text = text,
                    Experience = (ExperienceType)Enum.Parse(typeof(ExperienceType), experience),
                    Salary = new Salary()
                };

                unitOfWork.VacancyFilter.Create(VacancyFilter);
                unitOfWork.Save();

                logger.Info($"Выполнение команды {command.Description} завершено.");
                return 0;
            });
        }
    }
}
