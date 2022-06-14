using System;
using System.Collections.Generic;
using System.Text;

namespace VacancyAggregator.Domain.Interfaces
{
    /// <summary>
    /// Repository manager
    /// </summary>
    public interface IUnitOfWork: IDisposable
    {
        IVacancyRepository Vacancy { get; }
        IVacancyFilterRepository VacancyFilter { get; }
        IDataSourceRepository DataSource { get; }
        void Save();
    }
}
