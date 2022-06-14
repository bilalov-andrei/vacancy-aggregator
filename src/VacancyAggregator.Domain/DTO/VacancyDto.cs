using VacancyAggregator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyAggregator.Domain.DTO
{
    public class VacancyDto
    {
        public int Id { get; set; }

        public int VacancyFilterId { get; set; }

        public DataSourceDto DataSource { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string[] KeySkills { get; set; }

        public DateTime? PublishedAt { get; set; }

        public SalaryDto Salary { get; set; }

        public string Area { get; set; }

        public string Experience { get; set; }

        public string Status { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }

    public class SalaryDto
    {
        public int? From { get; set; }
        public int? To {get;set;}
        public string Currency { get; set; }
    }
}
