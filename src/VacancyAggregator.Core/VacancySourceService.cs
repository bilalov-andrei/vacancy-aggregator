using AutoMapper;
using NLog;
using VacancyAggregator.Core.Utils;
using VacancyAggregator.Domain.Interfaces;
using VacancyAggregator.Domain.Models;
using VacancyAggregator.VacancySources.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacancyFilter = VacancyAggregator.Domain.Models.VacancyFilter;
using Vacancy = VacancyAggregator.Domain.Models.Vacancy;

namespace VacancyAggregator.Core
{
    public class VacancySourceService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public VacancySourceService(ILogger logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public List<Vacancy> GetVacancies(DataSource dataSource, VacancyFilter VacancyFilter)
        {
            if (dataSource == null)
                throw new ArgumentNullException(nameof(dataSource));

            if (VacancyFilter == null)
                throw new ArgumentNullException(nameof(VacancyFilter));

            var workSource = AssemblyLoader.Load<IVacancySource>(dataSource.AssemblyPath, dataSource.ConnectionString);

            var apiVacancyFilter = _mapper.Map<VacancyAggregator.VacancySources.Api.VacancyFilter>(VacancyFilter);
            var apiVacancies = workSource.GetVacancies(apiVacancyFilter);
            var vacancies = _mapper.Map<List<Vacancy>>(apiVacancies);

            foreach(var vacancy in vacancies)
            {
                vacancy.VacancyFilterId = VacancyFilter.Id;
                vacancy.DataSourceId = dataSource.Id;
            }

            return vacancies;
        }
    }
}
