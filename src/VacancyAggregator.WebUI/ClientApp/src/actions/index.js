const vacanciesLoaded = (newVacancies) => {
    return {
        type: "VACANCIES_LOADED",
        payload: newVacancies
    }
}

const selectedFilterChanged = (selectedFilterId) => {
    return {
        type: 'SELECTED_VACANCY_FILTER_CHANGED',
        payload: selectedFilterId
    };
}

const vacancyFiltersLoaded = (newFilters) => {
    return {
        type: 'VACANCY_FILTERS_LOADED',
        payload: newFilters
    };
}

const vacancyItemSelected = (vacancyId) => {
    return {
        type: 'VACANCY_ITEM_SELECTED',
        payload: vacancyId
    };
}

const vacancyFiltersLoadedWithError = (error) => {
    return {
        type: 'VACANCY_FILTERS_LOADED_ERROR',
        payload: error
    }
}

const vacanciesLoadedWithError = (error) => {
    return {
        type: 'VACANCIES_LOADED_ERROR',
        payload: error
    }
}

export {
    vacanciesLoaded, vacancyFiltersLoaded, selectedFilterChanged,
    vacancyItemSelected, vacancyFiltersLoadedWithError, vacanciesLoadedWithError
}