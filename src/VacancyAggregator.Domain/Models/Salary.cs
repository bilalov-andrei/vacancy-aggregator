
namespace VacancyAggregator.Domain.Models
{
    public class Salary
    {
        public int? From { get; set; }
        public int? To { get; set; }
        public Currency Currency { get; set; }
    }
}
