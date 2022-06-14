using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacancyAggregator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace VacancyAggregator.Data.Configurations
{
    public class DataSourceConfiguration : IEntityTypeConfiguration<DataSource>
    {
        public void Configure(EntityTypeBuilder<DataSource> builder)
        {
            builder.HasData(new DataSource()
            {
                Id = 1,
                Name = "Internal",
                ShortName = "Inter",
                IsEnabled = true
            });
        }
    }
}
