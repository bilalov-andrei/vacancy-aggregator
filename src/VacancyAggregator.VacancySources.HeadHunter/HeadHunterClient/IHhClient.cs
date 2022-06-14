using VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient.Models;
using System.Collections.Generic;

namespace VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient
{
    internal interface IHhClient
    {
        List<HhVacancy> GetVacancyList(Dictionary<string, string> queryFilter);

        HhVacancy GetFullVacancy(string vacancyUrl);
    }
}