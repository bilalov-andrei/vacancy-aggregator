import React from 'react';
import { useSelector } from 'react-redux';
import './vacancy-detail.css';
import DOMPurify from 'dompurify'

function VacancyDetail() {

    const vacancy = useSelector(state => state.selectedVacancy);

    if (!vacancy)
        return <div className="vacancy-detail-container" >
            <div className='vacancy-detail'></div>
        </div>

    return (<div className="vacancy-detail-container" >
        <div className="vacancy-detail card">
            <div className="card-body">
                <h5 className="card-title">{vacancy.name}</h5>
                <div dangerouslySetInnerHTML={{ __html: DOMPurify.sanitize(vacancy.description) }}>
                </div>
            </div>
        </div>
    </div >)
}

export default VacancyDetail;