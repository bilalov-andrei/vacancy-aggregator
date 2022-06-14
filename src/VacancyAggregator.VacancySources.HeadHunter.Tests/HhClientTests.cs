using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient;
using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient.Models;
using VacancyAggregator.VacancySources.HeadHunter.Tests.TestData;
using Xunit;

namespace VacancyAggregator.VacancySources.HeadHunter.Tests
{
    public class HhClientTests
    {
        [Fact]
        public void HhClient_WhenResultOnFewPages_Returns_VacanciesFromAllPages()
        {
            //arrange
            var mockHttp = new MockHttpMessageHandler();
            HHVacancyBuilder hhVacancyBuilder = new HHVacancyBuilder();

            var pageZero = new RootPage()
            {
                items = Enumerable.Range(0, 100).Select(x => hhVacancyBuilder.Build()).ToArray(),
                page = 0,
                pages = 2
            };
            var pageOne = new RootPage()
            {
                items = Enumerable.Range(0, 50).Select(x => hhVacancyBuilder.Build()).ToArray(),
                page = 1,
                pages = 2
            };
            var pageZeroRootObjectString = JsonConvert.SerializeObject(pageZero);
            var pageOneRootObjectString = JsonConvert.SerializeObject(pageOne);
            mockHttp.When("http://localhost/vacancies?per_page=100&page=0")
                    .Respond("application/json", pageZeroRootObjectString);
            mockHttp.When("http://localhost/vacancies?per_page=100&page=1")
                    .Respond("application/json", pageOneRootObjectString);

            var client = new HhHttpClient(new HttpClient(mockHttp) { BaseAddress = new Uri("http://localhost/")});

            // Act
            var result = client.GetVacancyList(new Dictionary<string, string>());

            // Assert
            Assert.Equal(pageZero.items.Count() + pageOne.items.Count(), result.Count);
        }
    }
}
