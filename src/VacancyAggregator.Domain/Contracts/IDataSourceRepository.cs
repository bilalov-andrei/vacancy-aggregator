using VacancyAggregator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VacancyAggregator.Domain.Interfaces
{
    public interface IDataSourceRepository: IRepositoryBase<DataSource>
    {
        DataSource GetById(int id, bool trackChanges);
        Task<DataSource> GetByIdAsync(int id, bool trackChanges);
        Task<List<DataSource>> GetAllAsync(bool trackChanges);
    }
}
