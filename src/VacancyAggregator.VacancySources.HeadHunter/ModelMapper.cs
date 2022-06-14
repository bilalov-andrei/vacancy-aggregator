using System;
using System.Collections.Generic;
using System.Linq;
using VacancyAggregator.VacancySources.Api;
using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient.Models;

namespace VacancyAggregator.VacancySources.HeadHunter
{
    internal class ModelMapper
    { 
        public Vacancy Map(HhVacancy hhVacancy)
        {
            return new Vacancy(hhVacancy.Id, hhVacancy.Alternate_url, hhVacancy.Name)
            {
                Description = hhVacancy.Description,
                Experience = GetExperience(hhVacancy),
                PublishedAt = hhVacancy.published_at,
                Salary = GetSalary(hhVacancy),
                Schedules = GetShedule(hhVacancy),
                KeySkills = hhVacancy.key_skills?.Select(x => x.name).ToArray(),
                Status = GetStatus(hhVacancy),
            };
        }

        public Dictionary<string, string> MapToHhVacancyFilter(VacancyFilter VacancyFilter)
        {
            var hhVacancyFilter = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(VacancyFilter.Text))
            {
                hhVacancyFilter.Add("text", VacancyFilter.Text);
            }

            if (VacancyFilter.Area != null)
            {
                hhVacancyFilter.Add("area", VacancyFilter.Area.Id.ToString());
            }

            switch (VacancyFilter.Experience)
            {
                case ExperienceType.Does_not_matter:
                    break;
                case ExperienceType.No_experience:
                    hhVacancyFilter.Add("experience", "noExperience");
                    break;
                case ExperienceType.One_to_three:
                    hhVacancyFilter.Add("experience", "between1And3");
                    break;
                case ExperienceType.Three_to_six:
                    hhVacancyFilter.Add("experience", "between3And6");
                    break;
                case ExperienceType.More_than_six:
                    hhVacancyFilter.Add("experience", "moreThan6");
                    break;
            }

            return hhVacancyFilter;
        }

        private VacancyStatus GetStatus(HhVacancy hhVacancy)
        {
            if (hhVacancy.Archived)
                return VacancyStatus.Closed;

            switch (hhVacancy.Type.id)
            {
                case "open":
                case "anonymous":
                    return VacancyStatus.Open;
                case "closed":
                    return VacancyStatus.Closed;
                default:
                    throw new System.ArgumentException("Не распознан идентификатор Type у вакансии. " +
                        "Проверьте актуальность значений в справочниках");
            }

        }

        //TODO form area

        //TODO form all currencies
        private Api.Salary GetSalary(HhVacancy hhVacancy)
        {
            if (hhVacancy.Salary == null)
                return null;

            var salary = new Api.Salary()
            {
                From = hhVacancy.Salary.from,
                To = hhVacancy.Salary.to,
            };

            if (hhVacancy.Salary.currency == "RUR")
                salary.Currency = Currency.Rub;

            return salary;
        }

        private ExperienceType GetExperience(HhVacancy hhVacancy)
        {
            if (hhVacancy.Experience == null)
                return ExperienceType.Does_not_matter;

            switch (hhVacancy.Experience.id)
            {
                case "noExperience":
                    return ExperienceType.No_experience;
                case "between1And3":
                    return ExperienceType.One_to_three;
                case "between3And6":
                    return ExperienceType.Three_to_six;
                case "moreThan6":
                    return ExperienceType.More_than_six;
                default:
                    throw new System.ArgumentException("Не распознан идентификатор Experince. Проверьте актуальность значений в справочниках");
            }
        }

        private List<Api.Schedule> GetShedule(HhVacancy hhVacancy)
        {
            List<Api.Schedule> schedulesToReturn = new List<Api.Schedule>();
            var hhSchedule = hhVacancy.Schedule;

            if (hhSchedule == null)
            {
                schedulesToReturn.Add(Api.Schedule.notDefinded);
                return schedulesToReturn;
            }
            
            switch (hhSchedule.id)
            {
                case "fullDay":
                    schedulesToReturn.Add(Api.Schedule.fullDay);
                    break;
                case "remote":
                    schedulesToReturn.Add(Api.Schedule.remoteWork);
                    break;
                case "flexible":
                    schedulesToReturn.Add(Api.Schedule.flexible);
                    break;
                default:
                    throw new ArgumentException("Не распознан идентификатор Schedule. Проверьте актуальность значений в справочниках");
            }

            return schedulesToReturn;
        }

    }
}