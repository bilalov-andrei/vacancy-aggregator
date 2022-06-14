import React, { Component } from 'react';
import './nav-menu.css';

export class NavMenu extends Component {

  render() {
    return (
      <header>
        <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
          <div className="container-fluid">
            <a href='/' className="navbar-brand">Агрегатор вакансий</a>
          </div>
        </nav>
      </header >
    );
  }
}
