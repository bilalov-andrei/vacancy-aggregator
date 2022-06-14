using Microsoft.Extensions.Configuration;
using System;

namespace VacancyAggregator.Data
{
    internal class AppDbContextFactory
    {
        public AppDbContext Get(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("database:connectionString");
            var schemaName = configuration.GetValue<string>("database:schemaName");
            var provider = configuration.GetValue<string>("database:provider");

            AppDbContext swDbContext;

            switch (provider)
            {
                case "Postgres":
                    swDbContext = new PostgreSqlAppDbContext(connectionString, schemaName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Неподдерживаемый провайдер БД {provider}. Используйте Postgres.");
            }

            return swDbContext;
        }
    }
}
