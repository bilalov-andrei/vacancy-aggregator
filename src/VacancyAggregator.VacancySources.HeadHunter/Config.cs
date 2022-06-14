using System;
using System.Collections.Generic;
using System.Text;

namespace VacancyAggregator.VacancySources.HeadHunter
{
    internal class Config
    {
        public string BaseUrl { get; set; }

        public Config(string url)
        {
            this.BaseUrl = url;
        }
    }
}
