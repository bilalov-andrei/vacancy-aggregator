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
    public class ScheduleTests
    {
        [Fact]
        public void UnknownScheduleType_Returns_Exception()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetScheduleToUnknown).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            Action act = () => vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void NullScheduleType_Returns_UndefinedApiType()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetScheduleToNull).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            var experienceType = vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.All(experienceType, (x) => 
            {
                Assert.True(x.Schedules.Count == 1);
                Assert.True(x.Schedules.First() == Api.Schedule.notDefinded);
            });
        }

        [Fact]
        public void FullDayScheduleType_Returns_FullDayApiType()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetScheduleToFullDay).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            var experienceType = vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.All(experienceType, (x) =>
            {
                Assert.True(x.Schedules.Count == 1);
                Assert.True(x.Schedules.First() == Api.Schedule.fullDay);
            });
        }

        [Fact]
        public void FlexibleScheduleType_Returns_FlexibleApiType()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetScheduleToFlexible).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            var experienceType = vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.All(experienceType, (x) =>
            {
                Assert.True(x.Schedules.Count == 1);
                Assert.True(x.Schedules.First() == Api.Schedule.flexible);
            });
        }

        [Fact]
        public void RemoteScheduleType_Returns_RemoteApiType()
        {
            var vacancy = new HHVacancyBuilder().SetProperty(VacancyActions.SetScheduleToRemote).Build();
            IHhClient hhClient = Mock.Of<IHhClient>(
                   c => c.GetVacancyList(It.IsAny<Dictionary<string, string>>()) == new List<HhVacancy>() { vacancy }
                   && c.GetFullVacancy(It.IsAny<string>()) == vacancy);
            var vacancySource = new HeadHunterVacancySource(hhClient);

            var experienceType = vacancySource.GetVacancies(new Api.VacancyFilter()).ToList();

            Assert.All(experienceType, (x) =>
            {
                Assert.True(x.Schedules.Count == 1);
                Assert.True(x.Schedules.First() == Api.Schedule.remoteWork);
            });
        }
    }
}
