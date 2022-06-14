using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VacancyAggregator.Domain.Models
{

    public class Vacancy
    {
        public int Id { get; set; }

        [ForeignKey(nameof(DataSource))]
        public int DataSourceId { get; set; }

        [Required]
        public string ExternalId { get; set; }

        [ForeignKey(nameof(VacancyFilter))]
        public int VacancyFilterId { get; set; }

        [Required]
        public string ExternalUrl { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        [MaxLength(3000)]
        [Required]
        public string Description { get; set; }

        public string[] KeySkills { get; set; }

        public DateTime? PublishedAt { get; set; }

        public Salary Salary { get; set; }

        public string Area { get; set; }

        public ExperienceType Experience { get; set; }

        public VacancyStatus Status { get; set; }

        public ICollection<Schedule> Schedules { get; set; }

        public virtual VacancyFilter VacancyFilter { get; set; }

        public virtual DataSource DataSource { get; set; }

        public bool IsEqual(Vacancy other)
        {
            return this.DataSourceId == other.DataSourceId && this.ExternalId == other.ExternalId && this.VacancyFilterId == other.VacancyFilterId;
        }
    }

    public enum ExperienceType
    {
        /// <summary>
        /// Не имеет значения
        /// </summary>
        Does_not_matter,

        /// <summary>
        /// Без опыта
        /// </summary>
        No_experience,

        /// <summary>
        /// От 1 года до 3 лет
        /// </summary>
        One_to_three,

        /// <summary>
        /// От 3 лет до 6 лет
        /// </summary>
        Three_to_six,

        /// <summary>
        /// Более 6 лет
        /// </summary>
        More_than_six
    }

    public enum Schedule
    {
        /// <summary>
        /// Не указан
        /// </summary>
        notDefinded,

        /// <summary>
        /// Полный день
        /// </summary>
        fullDay,

        /// <summary>
        /// Частичная занятость
        /// </summary>
        partialDay,

        /// <summary>
        /// Удаленная работа
        /// </summary>
        remoteWork,

        /// <summary>
        /// Гибкий график
        /// </summary>
        flexible
    }

    public enum VacancyStatus
    {
        /// <summary>
        /// Вакансия открыта
        /// </summary>
        Open,

        /// <summary>
        /// Вакансия закрыта
        /// </summary>
        Closed,

        /// <summary>
        /// Вакансия не найдена(удалена)
        /// </summary>
        Deleted
    }

    public enum Currency
    {
        /// <summary>
        /// Российский рубль
        /// </summary>
        Rub,

        /// <summary>
        /// Евро
        /// </summary>
        Euro,

        /// <summary>
        /// Доллар США
        /// </summary>
        Dollar
    }
}
