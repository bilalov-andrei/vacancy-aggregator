import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './components/app/app';
import { Provider } from 'react-redux';
import store from './store';
import ErrorBoundry from './components/helpers/error-boundry/error-boundry';
import { VacancyServiceProvider } from './components/helpers/vacancy-service-context/vacancy-service-context'
import VacancyService from './services/vacancy-service';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

const vacancyService = new VacancyService();

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
    <Provider store={store}>
      <ErrorBoundry>
        <VacancyServiceProvider value={vacancyService}>
          <App />
        </VacancyServiceProvider>
      </ErrorBoundry>
    </Provider>
  </BrowserRouter>,
  rootElement);

