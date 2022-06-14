import React from "react";
import './vacancy-list-item.css';

const VacancyListItem = ({ vacancy, onItemSelected }) => {

    const salary = vacancy.salary?.to !== null ? vacancy.salary?.to + '' + vacancy.salary?.currency : '';

    let experience = '';
    switch (vacancy.experience) {
        case 'One_to_three':
            experience = "От 1 года до 3 лет"
            break;
        case 'Does_not_matter':
            experience = "Опыт не указан"
            break;
        case 'No_experience':
            experience = "Без опыта"
            break;
        case 'Three_to_six':
            experience = "От 3 до 6 лет"
            break;
        case 'More_than_six':
            experience = "Более 6 лет"
            break;
        default:
            return;
    }

    return (

        <div className="vacancy-list-item" onClick={() => onItemSelected(vacancy.id)}>
            <label href="">{vacancy.name}</label>
            <span className="badge rounded-pill bg-primary float-end">{vacancy.dataSource.shortName}</span>
            <p>{experience} {salary} {vacancy.area}</p>
        </div>)

}

export default VacancyListItem;