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
    public class DataSourceRepository : RepositoryBase<DataSource>, IDataSourceRepository
    {
        public DataSourceRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public DataSource GetById(int id, bool trackChanges)
        {
            return FindByCondition(x => x.Id == id && x.IsEnabled, trackChanges).SingleOrDefault();
        }

        public async Task<DataSource> GetByIdAsync(int id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id == id && x.IsEnabled, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<List<DataSource>> GetAllAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }
    }
}
