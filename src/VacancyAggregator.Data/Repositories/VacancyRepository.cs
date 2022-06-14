using Microsoft.EntityFrameworkCore;
using VacancyAggregator.Domain.DTO;
using VacancyAggregator.Domain.Interfaces;
using VacancyAggregator.Domain.Models;
using VacancyAggregator.Domain.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyAggregator.Data.Repositories
{
    public class VacancyRepository : RepositoryBase<Vacancy>, IVacancyRepository
    {
        public VacancyRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public void AddOrUpdate(Vacancy vacancy)
        {
            var dbVacation = FindByCondition(x => x.IsEqual(vacancy), false).SingleOrDefault();

            if(dbVacation == null)
            {
                Create(vacancy);
            }
            else
            {
                vacancy.Id = dbVacation.Id;
                Update(vacancy);
            }
        }

        public PagedList<Vacancy> GetAll(VacancyParameters parameters, bool trackChanges)
        {
            var vacancies = FindAll(trackChanges).Include(x => x.DataSource).OrderBy(x => x.Id);

            return PagedList<Vacancy>.ToPagedList(vacancies, parameters.PageNumber, parameters.PageSize);
        }

        public PagedList<Vacancy> GetAllByVacancyFilterId(int VacancyFilterId, VacancyParameters parameters, bool trackChanges)
        {
            var vacancies = FindByCondition(x => x.VacancyFilterId == VacancyFilterId, trackChanges).Include(x=>x.DataSource)
                .OrderBy(x => x.Id);

            return PagedList<Vacancy>.ToPagedList(vacancies, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<Vacancy>> GetAllAsync(VacancyParameters parameters,bool trackChanges)
        {
            var vacancies = await FindAll(trackChanges).Include(x => x.DataSource).OrderBy(x => x.Id).ToListAsync();

            return PagedList<Vacancy>.ToPagedList(vacancies, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<Vacancy>> GetAllByVacancyFilterIdAsync(int VacancyFilterId, VacancyParameters parameters, bool trackChanges)
        {
            var vacancies = await FindByCondition(x => x.VacancyFilterId == VacancyFilterId, trackChanges)
                .Include(x => x.DataSource)
                .OrderBy(x => x.Id)
                .ToListAsync();

            return PagedList<Vacancy>.ToPagedList(vacancies, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<Vacancy> GetByIdAsync(int id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id == id, trackChanges).Include(x => x.DataSource).SingleOrDefaultAsync();
        }
    }
}
