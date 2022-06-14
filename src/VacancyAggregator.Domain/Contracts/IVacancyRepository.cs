using System;
using System.Collections.Generic;
using VacancyAggregator.Domain.Models;
using System.Threading.Tasks;
using VacancyAggregator.Domain.DTO;
using VacancyAggregator.Domain.RequestParameters;

namespace VacancyAggregator.Domain.Interfaces
{
    public interface IVacancyRepository:IRepositoryBase<Vacancy>
    {
        void AddOrUpdate(Vacancy vacancy);
        PagedList<Vacancy> GetAll(VacancyParameters parameters, bool trackChanges);

        PagedList<Vacancy> GetAllByVacancyFilterId(int VacancyFilterId, VacancyParameters parameters, bool trackChanges);

        Task<PagedList<Vacancy>> GetAllAsync(VacancyParameters parameters, bool trackChanges);

        Task<PagedList<Vacancy>> GetAllByVacancyFilterIdAsync(int VacancyFilterId, VacancyParameters parameters, bool trackChanges);

        Task<Vacancy> GetByIdAsync(int id, bool trackChanges);
    }
}
