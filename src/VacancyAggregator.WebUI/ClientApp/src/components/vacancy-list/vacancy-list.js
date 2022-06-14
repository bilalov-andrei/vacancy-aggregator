import React, { Component } from "react";
import VacancyListItem from "../vacancy-list-item/vacancy-list-item";
import { connect } from "react-redux";
import withVacancyService from "../helpers/hoc/with-vacancy-service";
import { vacancyItemSelected, vacanciesLoaded } from '../../actions/index';
import './vacancy-list.css';
import Spinner from '../helpers/spinner/spinner';

class VacancyList extends Component {

    vacancyService = this.props.vacancyService;

    componentDidUpdate(prevProps) {

        const { vacanciesLoaded, selectedVacancyFilter } = this.props;

        if (prevProps.selectedVacancyFilter?.id !== selectedVacancyFilter?.id && selectedVacancyFilter) {
            this.vacancyService.getVacancies(selectedVacancyFilter?.id)
                .then(data => vacanciesLoaded(data));
        }
    }

    render() {

        const { vacancies, vacanciesLoading, vacancyItemSelected } = this.props;

        if (vacanciesLoading) {
            return <div id="vacancy-list">
                <Spinner text="Loading vacancies..." />
            </div >
        }

        const vacanciesRendered = vacancies.map(x => {
            return <li className="list-group-item list-group-item-action" key={x.id}><VacancyListItem vacancy={x} onItemSelected={vacancyItemSelected} /></li>
        })

        return (
            <div id="vacancy-list">
                <ul className="list-group w-100">
                    {vacanciesRendered}
                </ul>
            </div >)
    }
}

const mapStateToProps = ({ selectedVacancyFilter, vacancies, vacanciesLoading }) => { return { selectedVacancyFilter, vacancies, vacanciesLoading }; }

const mapDispatchToProps = {
    vacancyItemSelected,
    vacanciesLoaded
}

export default withVacancyService()(connect(mapStateToProps, mapDispatchToProps)(VacancyList));
