using VacancyAggregator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VacancyAggregator.Domain.Interfaces
{
    public interface IVacancyFilterRepository : IRepositoryBase<VacancyFilter>
    {
        IEnumerable<VacancyFilter> GetAllFilters(bool trackChanges);
        Task<IEnumerable<VacancyFilter>> GetAllFiltersAsync(bool trackChanges);

        VacancyFilter GetById(int id, bool trackChanges);
        Task<VacancyFilter> GetByIdAsync(int id, bool trackChanges);
    }
}
