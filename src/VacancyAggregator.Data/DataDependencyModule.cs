using Autofac;
using Microsoft.Extensions.Configuration;
using VacancyAggregator.Data;
using VacancyAggregator.Data.Repositories;
using VacancyAggregator.Domain.Interfaces;

namespace VacancyAggregator.Data
{
    public class DataDependencyModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContextFactory>().As<AppDbContextFactory>();


            builder.Register(
                (context, parameter) =>
                {
                    var swDbContextFactory = context.Resolve<AppDbContextFactory>();
                    var configuration = context.Resolve<IConfiguration>();
                    return swDbContextFactory.Get(configuration);
                })
                .As<AppDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}
