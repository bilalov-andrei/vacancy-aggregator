using NLog;
using VacancyAggregator.VacancySources.Api;
using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient.Models;

namespace VacancyAggregator.VacancySources.HeadHunter
{
    public class HeadHunterVacancySource: IVacancySource
    {
        private readonly IHhClient _headHunterClient;
        private readonly ModelMapper _modelMapper;
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public HeadHunterVacancySource(string connectionString)
        {
            Config config = ParseConfig(connectionString);

            HttpClient httpClient = new HhAuthService().GetNotAuthHttpClient(config.BaseUrl);
            _headHunterClient = new HhHttpClient(httpClient);

            _modelMapper = new ModelMapper();
        }

        internal HeadHunterVacancySource(IHhClient hhClient)
        {
            _headHunterClient = hhClient;
            _modelMapper = new ModelMapper();
        }

        public IEnumerable<Vacancy> GetVacancies(VacancyFilter VacancyFilter)
        {
            logger.Info("Выполнение запроса GetVacancies...");

            var hhVacancyFilter = _modelMapper.MapToHhVacancyFilter(VacancyFilter);
            var shortVacancies = _headHunterClient.GetVacancyList(hhVacancyFilter);

            //perform synchroniously to avoid spam endpoind and getting 403(forbidden)
            List<HhVacancy> fullVacancies = new List<HhVacancy>();
            foreach(var vacancy in shortVacancies)
            {
                fullVacancies.Add(_headHunterClient.GetFullVacancy(vacancy.url));
            }

            var result = fullVacancies.Select(_modelMapper.Map);

            logger.Info("Выполнение запроса GetVacancies завершено.");

            return result;
        }

        private Config ParseConfig(string str)
        {
            return new Config(str);
        }
    }
}
