const initialState = {
    vacancies: [],
    selectedVacancyFilter: null,
    selectedVacancy: null,
    vacancyFilters: [],
    vacancyFiltersLoading: true,
    vacanciesLoading: false,
    vacanciesLoadingError: null,
    vacancyFiltersLoadingError: null
}

const reducer = (state = initialState, action) => {
    switch (action.type) {
        case 'VACANCIES_LOADED':
            return {
                ...state,
                vacancies: action.payload,
                vacanciesLoading: false
            };
        case 'VACANCY_FILTERS_LOADED':
            return {
                ...state, vacancyFilters: action.payload,
                selectedVacancyFilter: null,
                vacancyFiltersLoading: false
            };
        case 'VACANCY_ITEM_SELECTED':
            return {
                ...state, selectedVacancy: state.vacancies.find(x => x.id == action.payload)
            }
        case 'SELECTED_VACANCY_FILTER_CHANGED':
            return {
                ...state, selectedVacancyFilter: state.vacancyFilters.find(x => x.id == action.payload),
                vacanciesLoading: true
            }
        case 'VACANCY_FILTERS_LOADED_ERROR':
            console.log(action.payload);
            return {
                ...state,
                vacancyFiltersLoading: false,
                vacancyFiltersLoadingError: action.payload
            }
        case 'VACANCIES_LOADED_ERROR':
            return {
                ...state,
                vacanciesLoadingError: action.payload
            }
        default:
            return state;
    }
};

export default reducer;