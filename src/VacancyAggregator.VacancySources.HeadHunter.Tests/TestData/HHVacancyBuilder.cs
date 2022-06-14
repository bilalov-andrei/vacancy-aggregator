using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient.Models;
using Type = VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient.Models.Type;

namespace VacancyAggregator.VacancySources.HeadHunter.Tests.TestData
{
    internal class HHVacancyBuilder
    {
        List<Action<HhVacancy>> propertyModifiers = new List<Action<HhVacancy>>();

        public HHVacancyBuilder SetProperty(Action<HhVacancy> propertySetter)
        {
            propertyModifiers.Add(propertySetter);
            return this;
        }

        public HhVacancy Build()
        {
            var vacancy = new HhVacancy()
            {
                Id = "1",
                Name = "name",
                Experience = new Experience()
                {
                    id = "noExperience",
                    name = "Нет опыта"
                },
                Alternate_url = "hh.ru",
                Type = new Type()
                {
                    id = "open",
                    name = "Открытая"
                },
                url = "hh.ru"
            };

            foreach(var propertyModifier in propertyModifiers)
            {
                propertyModifier.Invoke(vacancy);
            }

            return vacancy;
        }
    }
}
