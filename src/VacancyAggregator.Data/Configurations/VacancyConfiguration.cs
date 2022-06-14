using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using VacancyAggregator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VacancyAggregator.Data.Configurations
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.HasData(new Vacancy
            {
                Id = 1,
                DataSourceId = 1,
                VacancyFilterId = 1,
                Name = "first test vacancy",
                Description = "first description",
                Area = "moscow",
                ExternalId = "12",
                ExternalUrl = "https:\\site.ru",
                Experience = ExperienceType.Does_not_matter,
                KeySkills = new List<string>() { "c#", "js" }.ToArray(),
                PublishedAt = DateTime.UtcNow,
                Status = VacancyStatus.Open,
                Schedules = new List<Schedule>()
            });

            builder.OwnsOne(x => x.Salary).HasData(new
            {
                VacancyId = 1,
                Currency = Currency.Rub
            });

            var converter = new EnumCollectionJsonValueConverter<Schedule>();
            var comparer = new CollectionValueComparer<Schedule>();

            builder
              .Property(e => e.Schedules)
              .HasConversion(converter)
              .Metadata.SetValueComparer(comparer);

            builder
            .HasIndex(v => new { v.DataSourceId, v.ExternalId, v.VacancyFilterId })
            .IsUnique(true);

            builder
            .HasIndex(v => new { v.VacancyFilterId });
        }
    }

    internal class EnumCollectionJsonValueConverter<T> : ValueConverter<ICollection<T>, string> where T : Enum
    {
        public EnumCollectionJsonValueConverter() : base(
          v => JsonConvert
            .SerializeObject(v.Select(e => e.ToString()).ToList()),
          v => JsonConvert
            .DeserializeObject<ICollection<string>>(v)
            .Select(e => (T)Enum.Parse(typeof(T), e)).ToList())
        {
        }
    }

    internal class CollectionValueComparer<T> : ValueComparer<ICollection<T>>
    {
        public CollectionValueComparer() : base((c1, c2) => c1.SequenceEqual(c2),
          c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => (ICollection<T>)c.ToHashSet())
        {
        }
    }
}
