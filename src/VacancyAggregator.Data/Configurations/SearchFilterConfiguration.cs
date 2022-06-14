using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacancyAggregator.Domain.Models;

namespace VacancyAggregator.Data.Configurations
{
    public class VacancyFilterConfiguration : IEntityTypeConfiguration<VacancyFilter>
    {
        public void Configure(EntityTypeBuilder<VacancyFilter> builder)
        {
            builder.HasData(new VacancyFilter()
            {
                Id = 1,
                Text = "C#",
                Experience = ExperienceType.One_to_three,
            });

            builder.OwnsOne(x => x.Salary).HasData(new
            {
                VacancyFilterId = 1,
                Currency = Currency.Rub
            });

        }
    }
}
