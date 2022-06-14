using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VacancyAggregator.Domain.Models
{
    public class VacancyFilter
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Text { get; set; }

        public Salary Salary { get; set; }

        public string Area { get; set; }

        public ExperienceType Experience { get; set; }
    }
}
