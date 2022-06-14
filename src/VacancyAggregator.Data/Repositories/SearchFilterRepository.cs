using Microsoft.EntityFrameworkCore;
using VacancyAggregator.Domain.Interfaces;
using VacancyAggregator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyAggregator.Data.Repositories
{
    public class VacancyFilterRepository: RepositoryBase<VacancyFilter>, IVacancyFilterRepository
    {
        public VacancyFilterRepository(AppDbContext dbContext): base(dbContext)
        {

        }

        public IEnumerable<VacancyFilter> GetAllFilters(bool trackChanges)
        {
            return FindAll(trackChanges);
        }

        public async Task<IEnumerable<VacancyFilter>> GetAllFiltersAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public VacancyFilter GetById(int id, bool trackChanges)
        {
            return FindByCondition(x=>x.Id == id, trackChanges).SingleOrDefault();
        }

        public async Task<VacancyFilter> GetByIdAsync(int id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id == id, trackChanges).SingleOrDefaultAsync();
        }
    }
}
