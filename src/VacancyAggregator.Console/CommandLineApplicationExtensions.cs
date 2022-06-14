using Autofac;
using AutoMapper;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using VacancyAggregator.Core;
using System;

namespace VacancyAggregator.Console
{
    internal static class CommandLineApplicationExtensions
    {
        public static void ExecuteWithContainer(this CommandLineApplication command, Func<IContainer, int> invoke)
        {
            command.OnExecute(() =>
            {
                var container = GetContainer();
                return invoke(container);
            });
        }

        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            IConfiguration configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            builder.RegisterInstance(configuration).As<IConfiguration>().SingleInstance();

            builder.RegisterModule(new CoreDependencyModule());

            return builder.Build();
        }
    }
}
