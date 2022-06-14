import React, { Component } from "react";

export default class VacanciesPagination extends Component {
    render() {
        return (<nav aria-label="Page navigation example">
            <ul className="pagination justify-content-center">
                <li className="page-item disabled">
                    <span className="page-link" tabIndex="-1">Previous</span>
                </li>
                <li className="page-item"><span className="page-link">1</span></li>
                <li className="page-item"><span className="page-link">2</span></li>
                <li className="page-item"><span className="page-link">3</span></li>
                <li className="page-item">
                    <span className="page-link">Next</span>
                </li>
            </ul>
        </nav>);
    }
}