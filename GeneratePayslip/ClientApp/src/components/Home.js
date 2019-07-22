import React, { Component } from 'react';
import { GeneratePayslipFromFile } from './GeneratePayslipFromFile';
import { GenerateSingleEmployeePayslip } from './GenerateSingleEmployeePayslip';
import { Header, List } from 'semantic-ui-react';

//Render the main page to select the option to generate employees monthly payslip
export class Home extends Component {
    static displayName = Home.name;
    constructor(props) {
        super(props);
        this.state = {
            selectedValue: ""
        }
    }

    handleChange = (event) => {
        var data = event.target.value;
        this.setState({
            selectedValue: data
        })
    }

    render() {
        let selectedChoice = this.state.selectedValue;

        if (selectedChoice === "fromFile") {
            return <GeneratePayslipFromFile />
        }

        if (selectedChoice === "formValue") {
            return <GenerateSingleEmployeePayslip />
        }
        return (
            <React.Fragment>
                <Header size='medium'>Generate Employees Payslips</Header>
                <Header size='small'>Please select the option to generate employees payslips</Header>
                <List ordered>
                    <List.Item>From CSV file</List.Item>
                    <List.Item>Enter the employee details and generate the pay slip</List.Item>
                </List>
                <br />
                <select value={this.state.seletedValue} className="browser-default custom-select" onChange={this.handleChange}>
                    <option defaultValue="select">Please select one option</option>
                    <option value="fromFile">Select CSV file to get employees payslips</option>
                    <option value="formValue">Enter employee details to get payslip</option>
                </select>
            </React.Fragment>
        );
    }
}
