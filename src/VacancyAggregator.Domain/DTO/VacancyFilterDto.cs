using VacancyAggregator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyAggregator.Domain.DTO
{
    public class VacancyFilterDto
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public Salary Salary { get; set; }

        public string Area { get; set; }

        public ExperienceType Experience { get; set; }
    }
}
