import React from "react";
import { VacancyServiceConsumer } from '../vacancy-service-context/vacancy-service-context';

const withVacancyService = () => (Wrapped) => {
    return (props) => {
        return (<VacancyServiceConsumer>
            {
                (vacancyService) => {
                    return (<Wrapped {...props} vacancyService={vacancyService} />);
                }
            }
        </VacancyServiceConsumer>);
    }
};

export default withVacancyService;