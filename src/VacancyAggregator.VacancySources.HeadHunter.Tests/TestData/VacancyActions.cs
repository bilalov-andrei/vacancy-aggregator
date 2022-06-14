using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient.Models;
using System;
using Type = VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient.Models.Type;

namespace VacancyAggregator.VacancySources.HeadHunter.Tests.SetupData
{
    internal class VacancyActions
    {
        public static Action<HhVacancy> SetExperienceTypeToNoExperience = (HhVacancy vacancy) =>
        {
            vacancy.Experience = new Experience()
            {
                id = "noExperience",
                name = "Нет опыта"
            };
        };

        public static Action<HhVacancy> SetExperienceTypeToFromOneToThreeYears = (HhVacancy vacancy) =>
        {
            vacancy.Experience = new Experience()
            {
                id = "between1And3",
                name = "От 1 года до 3 лет"
            };
        };

        public static Action<HhVacancy> SetExperienceTypeFromThreeToSixYears = (HhVacancy vacancy) =>
        {
            vacancy.Experience = new Experience()
            {
                id = "between3And6",
                name = "От 3 до 6 лет"
            };
        };

        public static Action<HhVacancy> SetExperienceTypeMoreThanSixYears = (HhVacancy vacancy) =>
        {
            vacancy.Experience = new Experience()
            {
                id = "moreThan6",
                name = "Более 6 лет"
            };
        };

        public static Action<HhVacancy> SetExperienceTypeToUnknown = (HhVacancy vacancy) =>
        {
            vacancy.Experience = new Experience()
            {
                id = "unknown",
                name = "uknown"
            };
        };

        public static Action<HhVacancy> SetExperienceTypeToNull = (HhVacancy vacancy) =>
        {
            vacancy.Experience = null;
        };



        public static Action<HhVacancy> SetStatusTypeToNoUnknown = (HhVacancy vacancy) =>
        {
            vacancy.Type = new Type()
            {
                id = "unknown",
                name = "unknown"
            };
        };

        public static Action<HhVacancy> SetStatusTypeToOpen = (HhVacancy vacancy) =>
        {
            vacancy.Type = new Type()
            {
                id = "open",
                name = "Открытая"
            };
        };

        public static Action<HhVacancy> SetStatusTypeToArchived = (HhVacancy vacancy) =>
        {
            vacancy.Archived = true;
        };

        public static Action<HhVacancy> SetStatusTypeToClosed = (HhVacancy vacancy) =>
        {
            vacancy.Type = new Type()
            {
                id = "closed",
                name = "Закрытая"
            };
        };




        public static Action<HhVacancy> SetScheduleToFullDay = (HhVacancy vacancy) =>
        {
            vacancy.Schedule = new Schedule()
            {
                id = "fullDay",
                name = "Полный день"
            };
        };

        public static Action<HhVacancy> SetScheduleToFlexible = (HhVacancy vacancy) =>
        {
            vacancy.Schedule = new Schedule()
            {
                id = "flexible",
                name = "Гибкий график"
            };
        };

        public static Action<HhVacancy> SetScheduleToRemote = (HhVacancy vacancy) =>
        {
            vacancy.Schedule = new Schedule()
            {
                id = "remote",
                name = "Удаленная работа"
            };
        };

        public static Action<HhVacancy> SetScheduleToNull = (HhVacancy vacancy) =>
        {
            vacancy.Schedule = null;
        };

        public static Action<HhVacancy> SetScheduleToUnknown = (HhVacancy vacancy) =>
        {
            vacancy.Schedule = new Schedule()
            {
                id = "unknown",
                name = "unknown"
            };
        };
    }
}
