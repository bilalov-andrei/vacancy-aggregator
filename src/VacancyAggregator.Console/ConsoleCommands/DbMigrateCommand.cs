using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.CommandLineUtils;
using NLog;
using VacancyAggregator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VacancyAggregator.Console.ConsoleCommands
{
    //TODO Better to move this command from public to internal level - for engeneers, not for users
    internal class DbMigrateCommand
    {
        public static void Register(CommandLineApplication app)
        {
            app.Command("db-migrate", Setup);
        }

        private static void Setup(CommandLineApplication command)
        {
            command.HelpOption("-?|-h|--help");
            command.Description = "Обновление схемы базы данных.";

            command.ExecuteWithContainer((container) =>
            {
                var logger = container.Resolve<ILogger>();

                logger.Info("Выполнение команды " + command.Description);

                using (AppDbContext db = container.Resolve<AppDbContext>())
                {
                    var migrator = db.GetService<IMigrator>();
                    var pendingMigrations = db.Database.GetPendingMigrations().ToList();

                    if (!pendingMigrations.Any())
                    {
                        logger.Info($"Все изменения уже применены");
                        return 0;
                    }

                    foreach (var migration in pendingMigrations)
                    {
                        logger.Info($"Применение миграции: {migration}");
                        migrator.Migrate(migration);
                    }
                    logger.Info("Все изменения применены");
                }

                logger.Info("Обновление схемы базы данных успешно завершено.");
                return 0;
            });
        }
    }
}
