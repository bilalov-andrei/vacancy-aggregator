using VacancyAggregator.VacancySources.HeadHunter.Tests.SetupData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient;
using Moq;
using VacancyAggregator.VacancySources.HeadHunter.Tests.TestData;
using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient.Models;

namespace VacancyAggregator.VacancySources.HeadHunter.Tests
{
    public class StatusTests
    {
        [Fact]
        public void UnknownStatusType_Returns_Exception()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetStatusTypeToNoUnknown).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            Action act = () => vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void OpenStatusType_Returns_OpenApiType()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetStatusTypeToOpen).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            var vacancies = vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.All(vacancies, (x) => { Assert.True(x.Status == Api.VacancyStatus.Open); });
        }

        [Fact]
        public void ClosedStatusType_Returns_ClosedApiType()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetStatusTypeToClosed).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            var vacancies = vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.All(vacancies, (x) => { Assert.True(x.Status == Api.VacancyStatus.Closed); });
        }

        [Fact]
        public void ArchivedStatusType_Returns_ArchivedApiType()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetStatusTypeToArchived).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            var vacancies = vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.All(vacancies, (x) => { Assert.True(x.Status == Api.VacancyStatus.Closed); });
        }

    }
}
