using System;

namespace VacancyAggregator.VacancySources.HeadHunter.HeadHunterClient.Models
{
    internal class RootPage
    {
        public HhVacancy[] items { get; set; }
        public int found { get; set; }
        public int pages { get; set; }
        public int per_page { get; set; }
        public int page { get; set; }
        public object clusters { get; set; }
        public object arguments { get; set; }
        public string alternate_url { get; set; }
    }


    internal class HhVacancy
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Area area { get; set; }
        public Salary Salary { get; set; }
        public Type Type { get; set; }
        public Experience Experience { get; set; }
        public Schedule Schedule { get; set; }
        public string Description { get; set; }
        public Key_Skills[] key_skills { get; set; }
        public bool Archived { get; set; }
        public string url { get; set; }
        public DateTime? published_at { get; set; }
        public Specialization[] specializations { get; set; }
        public Professional_Roles[] professional_roles { get; set; }
        public bool hidden { get; set; }
        public string Alternate_url { get; set; }
    }

}
