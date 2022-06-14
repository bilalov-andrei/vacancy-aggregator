using VacancyAggregator.VacancySources.HeadHunter.Tests.SetupData;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient;
using VacancyAggregator.VacancySources.HeadHunter.Tests.TestData;
using Moq;
using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient.Models;

namespace VacancyAggregator.VacancySources.HeadHunter.Tests
{
    public class ExperienceTypesTests
    {
        [Fact]
        public void UnknownExperienceType_Returns_Exception()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetExperienceTypeToUnknown).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            Action act = () => vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void NullExperienceType_Returns_DoesNotMatterApiType()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetExperienceTypeToNull).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            var vacancies = vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.All(vacancies, (x) => { Assert.True(x.Experience == Api.ExperienceType.Does_not_matter); });
        }

        [Fact]
        public void ExperienceType_NotExperience_Returns_NoExperienceApiType()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetExperienceTypeToNoExperience).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            var vacancies = vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.All(vacancies, (x) => { Assert.True(x.Experience == Api.ExperienceType.No_experience); });
        }

        [Fact]
        public void ExperienceType_FromOneYearToThreeYears_Returns_OneToThreeApiType()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetExperienceTypeToFromOneToThreeYears).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            var vacancies = vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.All(vacancies, (x) => { Assert.True(x.Experience == Api.ExperienceType.One_to_three); });
        }

        [Fact]
        public void ExperienceType_BetweenThreeToSixYears_returns_ThreeToSixYearsApiType()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetExperienceTypeFromThreeToSixYears).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            var vacancies = vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.All(vacancies, (x) => { Assert.True(x.Experience == Api.ExperienceType.Three_to_six); });
        }

        [Fact]
        public void ExperienceType_MoreThanSixYears_returns_MoreThanSixYearsApiType()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetExperienceTypeMoreThanSixYears).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            var vacancies = vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.All(vacancies, (x) => { Assert.True(x.Experience == Api.ExperienceType.More_than_six); });
        }
    }
}