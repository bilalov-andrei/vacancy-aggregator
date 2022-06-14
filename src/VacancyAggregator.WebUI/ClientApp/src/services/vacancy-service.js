class VacancyService {

    async getVacancies(filterId) {
        return await fetch("https://localhost:5001/api/v1/Vacancies/byVacancyFilter/" + filterId)
            .then((response) => { if (response.ok) return response.json(); else throw new Error(response.statusText) });
    }

    async getVacancyFilters() {
        return await fetch("https://localhost:5001/api/v1/VacancyFilters")
            .then((response) => { if (response.ok) return response.json(); else throw new Error(response.statusText) });
    }
}

export default VacancyService;