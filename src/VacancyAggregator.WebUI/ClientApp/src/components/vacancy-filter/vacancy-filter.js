import React, { useEffect } from "react";
import './vacancy-filter.css'
import withVacancyService from "../helpers/hoc/with-vacancy-service";
import { selectedFilterChanged, vacancyFiltersLoaded, vacancyFiltersLoadedWithError } from "../../actions";
import { connect } from "react-redux";
import Spinner from '../helpers/spinner/spinner';
import ErrorIndicator from "../helpers/error-indicator/error-indicator";

const VacancyFilter = function (props) {

    const { vacancyService, vacancyFiltersLoading, vacancyFilters, selectedVacancyFilter, vacancyFiltersLoaded,
        selectedFilterChanged, vacancyFiltersLoadedWithError, vacancyFiltersLoadingError } = props;

    useEffect(() => {
        vacancyService.getVacancyFilters()
            .then(data => vacancyFiltersLoaded(data))
            .catch(error => vacancyFiltersLoadedWithError(error));
    }, []);

    const selectedFilterChangedEvent = (e) => {
        selectedFilterChanged(e.target.value);
    }

    if (vacancyFiltersLoading)
        return <Spinner text="Loading vacancy filters..." />

    if (vacancyFiltersLoadingError) {
        return <ErrorIndicator text="Error of getting vacancy filters from server" />
    }

    const renderedVacancyFilters = vacancyFilters.map(x => {
        return <option key={x.id} value={x.id}>{x.text}</option>
    })

    return (
        <select onChange={selectedFilterChangedEvent} value={selectedVacancyFilter?.id} className="form-select d-flex justify-content-center vacancies-filter" aria-label="Default select example">
            <option>Выберите фильтр для вакансий</option>
            {renderedVacancyFilters}
        </select>

    );
}

const mapDispatchToProps = { vacancyFiltersLoaded, selectedFilterChanged, vacancyFiltersLoadedWithError };
const mapStateToProps = ({ vacancyFilters, selectedVacancyFilter, vacancyFiltersLoading, vacancyFiltersLoadingError }) => { return { vacancyFiltersLoading, vacancyFilters, selectedVacancyFilter, vacancyFiltersLoadingError }; }

export default withVacancyService()(connect(mapStateToProps, mapDispatchToProps)(VacancyFilter));
