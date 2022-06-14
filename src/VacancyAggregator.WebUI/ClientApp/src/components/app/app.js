import React, { Component } from 'react';
import { Layout } from '../layout/layout';
import VacancyFilter from "../vacancy-filter/vacancy-filter";
import VacancyPagination from "../vacancy-pagination/vacancy-pagination";
import VacancyList from "../vacancy-list/vacancy-list";
import VacancyDetail from '../vacancy-detail/vacancy-detail';

import './app.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <div className='main-container'>
          <div>
            <VacancyFilter />
            <VacancyList />
            <VacancyPagination />
          </div>
          <VacancyDetail />
        </div>
      </Layout>
    );
  }
}
