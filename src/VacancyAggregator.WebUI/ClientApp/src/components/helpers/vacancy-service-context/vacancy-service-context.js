import React from "react";

const {
    Provider: VacancyServiceProvider,
    Consumer: VacancyServiceConsumer
} = React.createContext();

export { VacancyServiceConsumer, VacancyServiceProvider };