using System;
using System.Collections.Generic;
using System.Text;

namespace VacancyAggregator.VacancySources.Api
{
    public interface IVacancySource
    {
        IEnumerable<Vacancy> GetVacancies(VacancyFilter VacancyFilter);
    }
}
