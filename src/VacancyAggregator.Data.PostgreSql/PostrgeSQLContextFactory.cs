using Autofac;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using VacancyAggregator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace VacancyAggregator.Data.PostgreSql
{
    public class PostrgesSQLContextFactory : IDesignTimeDbContextFactory<PostgreSqlAppDbContext>
    {
        public PostgreSqlAppDbContext CreateDbContext(string[] args)
        {
            var container = GetContainer();
            var context = container.Resolve<AppDbContext>() as PostgreSqlAppDbContext;

            return context;
        }

        private IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(GetConfiguration()).As<IConfiguration>().SingleInstance();
            builder.RegisterModule(new DataDependencyModule());

            return builder.Build();
        }

        private IConfiguration GetConfiguration()
        {
            const string sw_env = "VACANCY_AGGREGATOR_CONFIG";

            var swEnvPath = Environment.GetEnvironmentVariable(sw_env);

            if (swEnvPath == null)
            {
                throw new ArgumentNullException($"Не установлена системная переменная {sw_env}");
            }

            var configurationBuilder = new ConfigurationBuilder().AddJsonFile(swEnvPath);

            return configurationBuilder.Build();
        }
    }
}
