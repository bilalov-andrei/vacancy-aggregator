using System;
using Autofac;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using NLog;
using VacancyAggregator.Data;

namespace VacancyAggregator.Core
{
    public class CoreDependencyModule : Module
    {
        public CoreDependencyModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            var logger = LogManager.GetCurrentClassLogger();

            builder.RegisterInstance(logger).As<ILogger>();
            builder.RegisterType<VacancySourceService>();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();

            builder.RegisterModule<DataDependencyModule>();
        }
    }
}
