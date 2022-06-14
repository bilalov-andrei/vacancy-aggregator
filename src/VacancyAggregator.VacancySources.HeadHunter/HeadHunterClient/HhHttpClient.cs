using Newtonsoft.Json;
using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient
{
    internal class HhHttpClient: IHhClient
    {
        private readonly HttpClient _httpClient;
        public HhHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Получение списка вакансий, соотвествующих по поисковому фильтру. 
        /// </summary>
        /// <returns>Список вакансий без заполненного свойства description</returns>
        public List<HhVacancy> GetVacancyList(Dictionary<string,string> vacancyFilter)
        {
            vacancyFilter.Add("per_page", "100");
            string queryStr = vacancyFilter.Aggregate(new StringBuilder(),
                                    (result, item) =>
                                    {
                                        return result.AppendFormat("{0}{1}={2}", result.Length > 0 ? "&" : "", item.Key, WebUtility.UrlEncode(item.Value));

                                    }, sb => sb.ToString());

            var shortVacancies = ProcessLoopGet(queryStr, new List<HhVacancy>(), 0);

            return shortVacancies;
        }

        public HhVacancy GetFullVacancy(string vacancyUrl)
        {
            using (var response = _httpClient.GetAsync(vacancyUrl).Result)
            {
                response.EnsureSuccessStatusCode();

                var responseContent = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<HhVacancy>(responseContent);
            }
        }

        private List<HhVacancy> ProcessLoopGet(string queryString, List<HhVacancy> accumulatedData, int startPage = 0)
        {
            using (var response = _httpClient.GetAsync("vacancies?" + queryString + "&page=" + startPage).Result)
            {
                response.EnsureSuccessStatusCode();

                var responseContent = response.Content.ReadAsStringAsync().Result;

                var items = JsonConvert.DeserializeObject<RootPage>(responseContent);

                accumulatedData.AddRange(items.items);

                if (items.pages > 1 && items.page < items.pages - 1)
                {
                    ProcessLoopGet(queryString, accumulatedData,++startPage);
                }

                return accumulatedData;
            }
        }
    }
}