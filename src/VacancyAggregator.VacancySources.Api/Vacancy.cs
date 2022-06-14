using System;
using System.Collections.Generic;
using System.Text;

namespace VacancyAggregator.VacancySources.Api
{
    /// <summary>
    /// Вакансия
    /// </summary>
    public sealed class Vacancy
    {
        public Vacancy(string externalId, string externalUrl, string name)
        {
            if (string.IsNullOrWhiteSpace(externalId))
                throw new ArgumentNullException(nameof(externalId));

            if (string.IsNullOrWhiteSpace(externalUrl))
                throw new ArgumentNullException(nameof(externalUrl));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            this.ExternalId = externalId;
            this.ExternalUrl = externalUrl;
            this.Name = name;
        }

        /// <summary>
        /// Идентификатор вакансии в источнике
        /// </summary>
        public string ExternalId { get; private set; }

        /// <summary>
        /// Внешняя ссылка на вакансию
        /// </summary>
        public string ExternalUrl { get; private set; }

        /// <summary>
        /// Наименование вакансии
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Описание вакансии
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Требуемые ключевые навыки работника
        /// </summary>
        public string[] KeySkills { get; set; }

        /// <summary>
        /// Дата публикации вакансии
        /// </summary>
        public DateTime? PublishedAt { get; set; }

        /// <summary>
        /// Предлагаемая зарплата
        /// </summary>
        public Salary Salary { get; set; }

        /// <summary>
        /// География действия вакансии
        /// </summary>
        public Area Area { get; set; }

        /// <summary>
        /// Накопленный опыт
        /// </summary>
        public ExperienceType Experience { get; set; }

        /// <summary>
        /// Состояние вакансии
        /// </summary>
        public VacancyStatus Status { get; set; }

        /// <summary>
        /// График работы
        /// </summary>
        public List<Schedule> Schedules { get; set; }
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

    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }

    public class Salary
    {
        public int? From { get; set; }
        public int? To { get; set; }
        public Currency Currency { get; set; }
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
}
