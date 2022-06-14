using VacancyAggregator.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VacancyAggregator.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContext;
        private IVacancyRepository vacancyRepository;
        private IVacancyFilterRepository VacancyFilterRepository;
        private IDataSourceRepository dataSourceRepository;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IVacancyRepository Vacancy
        {
            get
            {
                if(vacancyRepository == null)
                    vacancyRepository = new VacancyRepository(_dbContext);

                return vacancyRepository;
            }
        }

        public IVacancyFilterRepository VacancyFilter
        {
            get
            {
                if (VacancyFilterRepository == null)
                    VacancyFilterRepository = new VacancyFilterRepository(_dbContext);

                return VacancyFilterRepository;
            }
        }

        public IDataSourceRepository DataSource
        {
            get
            {
                if (dataSourceRepository == null)
                    dataSourceRepository = new DataSourceRepository(_dbContext);

                return dataSourceRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
