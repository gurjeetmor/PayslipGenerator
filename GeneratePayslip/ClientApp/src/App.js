import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { GeneratePayslipFromFile } from './components/GeneratePayslipFromFile';
import { GenerateSingleEmployeePayslip } from './components/GenerateSingleEmployeePayslip';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/generatePayslipFromFile' component={GeneratePayslipFromFile} />
        <Route path='/generateSingleEmployeePayslip' component={GenerateSingleEmployeePayslip} />
      </Layout>
    );
  }
}
