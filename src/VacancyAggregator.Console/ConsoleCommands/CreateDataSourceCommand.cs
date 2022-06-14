using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NLog;
using VacancyAggregator.Domain.Interfaces;
using VacancyAggregator.Domain.Models;

namespace VacancyAggregator.Console.ConsoleCommands
{
    internal class CreateDataSourceCommand
    {
        public static void Register(CommandLineApplication app)
        {
            app.Command("create-datasource", Setup);
        }

        private static void Setup(CommandLineApplication command)
        {
            command.HelpOption("-?|-h|--help");
            command.Description = "Добавление нового источника вакансий в базу данных VacancyAggregator";

            var dataSourceNameOption = command.Option(
                 "-n|--name",
                 "Имя источника данных",
                 CommandOptionType.SingleValue);

            var dataSourceShortNameOption = command.Option(
                "-sn|--name",
                "Краткое имя источника данных",
                CommandOptionType.SingleValue);

            var assemblyPathOption = command.Option(
                 "-a|--assemply-path",
                 "Путь к библиотеке, реализующей интерфейс IWorkSource",
                 CommandOptionType.SingleValue);

            var connStringOption = command.Option(
                   "-c|--conn-string",
                   "Строка подключения для объекта, реализующего IWorkSource.",
                   CommandOptionType.SingleValue);


            command.ExecuteWithContainer((container) =>
            {
                var logger = container.Resolve<ILogger>();

                logger.Info($"Выполнение команды {command.Description}.");

                if (!dataSourceNameOption.HasValue())
                {
                    throw new CommandParsingException(command, "Не указано имя источника данных.");
                }

                if (!dataSourceShortNameOption.HasValue())
                {
                    throw new CommandParsingException(command, "Не указано краткое имя источника данных.");
                }

                if (!assemblyPathOption.HasValue())
                {
                    throw new CommandParsingException(command, "Не указан путь к библиотеке.");
                }

                if (!connStringOption.HasValue())
                {
                    throw new CommandParsingException(command, "Не указана строка подключения.");
                }

                var unitOfWork = container.Resolve<IUnitOfWork>();

                var dataSourceName = dataSourceNameOption.Value();
                var shortDataSourceName = dataSourceShortNameOption.Value();
                var assemblyPath = assemblyPathOption.Value();
                var connString = connStringOption.Value();

                DataSource dataSource = new DataSource()
                {
                    Name = dataSourceName,
                    ShortName = shortDataSourceName,
                    AssemblyPath = assemblyPath,
                    ConnectionString = connString,
                    IsEnabled = true
                };

                unitOfWork.DataSource.Create(dataSource);
                unitOfWork.Save();

                logger.Info($"Выполнение команды {command.Description} завершено.");
                return 0;
            });
        }
    }
}
