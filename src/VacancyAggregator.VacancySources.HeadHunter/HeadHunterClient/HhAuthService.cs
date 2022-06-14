using System;
using System.Net.Http;

namespace VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient
{
    internal class HhAuthService
    {
        /// <summary>
        /// Используется, когда авторизация не требуется для выполнения методов api
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public HttpClient GetNotAuthHttpClient(string baseUrl)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36" +
                " (KHTML, like Gecko) Chrome/102.0.5005.63 Safari/537.36");

            return httpClient;
        }
    }
}