using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VacancyAggregator.Domain.Models
{
    public class DataSource
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string ShortName { get; set; }

        public string AssemblyPath { get; set; }

        //TODO store as encripted in database
        public string ConnectionString { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsExternal
        {
            get
            {
                return !string.IsNullOrEmpty(AssemblyPath);
            }
        }
    }
}
