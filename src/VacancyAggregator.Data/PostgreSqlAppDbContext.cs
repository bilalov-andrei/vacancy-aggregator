using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace VacancyAggregator.Data
{
    public class PostgreSqlAppDbContext:AppDbContext
    {
        public PostgreSqlAppDbContext(string connectionString, string schemaName) : base(connectionString, schemaName)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString, options =>
            {
                options
                .MigrationsAssembly("VacancyAggregator.Data.PostgreSql")
                .MigrationsHistoryTable("__EFMigrationsHistory", _schemaName);
            });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!Database.IsNpgsql())
            {
                // проверка при запуске через VacancyAggregator.Console
                throw new Exception("Миграции используются с несовместимым провайдером");
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
