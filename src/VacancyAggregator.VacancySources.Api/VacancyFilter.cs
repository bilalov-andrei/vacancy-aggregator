using VacancyAggregator.VacancySources.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace VacancyAggregator.VacancySources.Api
{
    /// <summary>
    /// фильтрация запроса для ограничения выборки
    /// </summary>
    public class VacancyFilter
    {
        public int Id { get; set; }

        /// <summary>
        /// Ограничение по ключевым словам
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Ограничение по зарплате
        /// </summary>
        public Salary Salary { get; set; }

        /// <summary>
        /// Ограничение по регионам и городам
        /// </summary>
        public Area Area { get; set; }

        /// <summary>
        /// Ограничение по опыту
        /// </summary>
        public ExperienceType Experience { get; set; }
    }
}
