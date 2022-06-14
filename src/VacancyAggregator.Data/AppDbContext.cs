using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using VacancyAggregator.Data.Configurations;
using VacancyAggregator.Domain.Models;

namespace VacancyAggregator.Data
{
    public abstract class AppDbContext: DbContext
    {
        protected string _schemaName = "VACANCY_AGGREGATOR_SCHEMA";
        protected readonly string _connectionString;

        public virtual DbSet<Vacancy> Vacancies { get; set; }
        public virtual DbSet<DataSource> DataSources { get; set; }
        public virtual DbSet<VacancyFilter> VacancyFilters { get; set; }

        public AppDbContext(string connectionString, string schemaName)
        {
            _connectionString = connectionString;
            _schemaName = schemaName;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schemaName);

            modelBuilder.ApplyConfiguration(new VacancyConfiguration());
            modelBuilder.ApplyConfiguration(new DataSourceConfiguration());
            modelBuilder.ApplyConfiguration(new VacancyFilterConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected void ConfigureProvider<TBuilder, TExtension>(RelationalDbContextOptionsBuilder<TBuilder, TExtension> optionsBuilder)
            where TBuilder : RelationalDbContextOptionsBuilder<TBuilder, TExtension>
            where TExtension : RelationalOptionsExtension, new()
        {
            optionsBuilder.MigrationsHistoryTable("__EFMigrationsHistory", _schemaName);
        }
    }

}
