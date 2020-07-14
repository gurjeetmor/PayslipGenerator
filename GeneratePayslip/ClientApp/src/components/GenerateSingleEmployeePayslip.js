import React, { Component } from 'react';
import { Form, Button, Header, Input } from 'semantic-ui-react';
import $ from 'jquery';
import './NavMenu.css';

//This component is used to display the employee detail form, allow user to submit employee details.
//Display the processed employees payslip information
export class GenerateSingleEmployeePayslip extends Component {
    constructor(props) {
        super(props);

        this.state = {
            errors: {},
            employeeData: [],
            employeePayslipsData:[],
            firstName: '',
            lastName: '',
            salary: '',
            superRate: '',
            startDate: new Date(),
            endDate: new Date(),
            submitEmployeeDetails: false           
        };
        this.baseState = this.state 
    }

    //Handle the form fields changes called when user change the any field, set the field states 
    handleChange(event) {      
        this.setState({
            errors: {}
        });
            this.setState({
                [event.target.name]: event.target.value
        })
    }

    //Reset the form fields state
    resetForm = () => {
        this.setState(this.baseState)
    }

    //Method use to validate form fields when subbmitting employees information
    validateForm() {
        let errors = {}
        let formIsValid = true
        if (!this.state.firstName) {
            formIsValid = false;
            errors['firstName'] = '*Please enter the Employee First Name.';
        }
        if (!this.state.lastName) {
            formIsValid = false;
            errors['lastName'] = '*Please enter the Employee Last Name.';
        }
        if (!this.state.salary) {
            formIsValid = false;
            errors['salary'] = '*Please enter annual salary'
        }
        if (typeof this.state.salary !== "undefined") {
            if (!this.state.salary.match(/^\d+(\.\d{1,2})?$/)) {
                formIsValid = false;
                errors["salary"] = "*Please enter numbers only.";
            }
        }
        if (!this.state.superRate) {
            formIsValid = false;
            errors['superRate'] = '*Please enter super rate between 0 to 50'
        }
        if (typeof this.state.superRate !== "undefined") {
            if (!this.state.superRate.match(/^\d+(\.\d{1,2})?$/) || parseInt(this.state.superRate)>51) {
                formIsValid = false;
                errors["superRate"] = "*Please enter positive numbers with value less than 50.";
            }
        }
        if (!this.state.startDate) {
            formIsValid = false;
            errors['startDate'] = '*Please provide the start date.'
        } 
        if (!this.state.endDate || this.state.startDate >= this.state.endDate) {
            formIsValid = false;
            errors['endDate'] = '*Please select end date greater than start date.'
        }
        if (new Date(this.state.endDate).getMonth() !== new Date(this.state.startDate).getMonth()) {
            formIsValid = false;
            errors['startDate'] = '*Please select same month for Payment period.'
            errors['endDate'] = '*Please start one same for Payment period.'                    
        }
        

        this.setState({
            errors: errors
        });
        return formIsValid
    }

    //Method used to sends the submitted employee or employees information to controller method
    //DisplayPayslips via AJAX call to process and generate payslips.
    finalSubmit=(e)=> {
        e.preventDefault();
        if (this.validateForm()) {
            let { employeeData } = this.state;
            employeeData.push({
                'firstName': this.state.firstName,
                'lastName': this.state.lastName,
                'salary': this.state.salary,
                'superRate': this.state.superRate,
                'startDate': this.state.startDate,
                'endDate': this.state.endDate
            })
            this.setState({ employeeData: employeeData });
            console.log("data", JSON.stringify(employeeData));

            $.ajax({
                url: 'api/PayslipGenerator/DisplayPayslips?employee=' + JSON.stringify(this.state.employeeData),
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(this.state.employeeData),
                success: function (res) {
                    if (res.success) {
                        this.setState({
                            employePayslipsData: res.paySlips
                        })
                    } else {
                        console.log("Error in getting employee payslip", res.message)
                    }

                }.bind(this),
                error: function (res) {
                    console.log("Error in application call", res)
                }
            });
            this.setState({
                submitEmployeeDetails: true
            })
        }
    }

    //This method used to add more employees detail through interactive Form UI
    //Allow the user to submit one or more than one employees details at once.
    addMoreEmployee = () => {

        if (this.validateForm()) {
            let { employeeData } = this.state;
            employeeData.push({
                firstName: this.state.firstName,
                lastName: this.state.lastName,
                salary: this.state.salary,
                superRate: this.state.superRate,
                startDate: this.state.startDate,
                endDate: this.state.endDate
            })
            this.setState({ employeeData: employeeData })
            document.getElementById("employeePayDetailForm").reset();
            this.resetForm();
        }
    }

    //Render the views to enter employees information or to display the processed employees payslips 
    render() {
        let employeesPaySlip = null;
        let employeePaySlipsList = this.state.employePayslipsData;

        if (employeePaySlipsList != null) {
            employeesPaySlip = employeePaySlipsList.map((employee, index) =>
                <tr key={index}>
                    <td className="two wide">{employee.name}</td>
                    <td className="two wide">{employee.payPeriod}</td>
                    <td className="two wide">{employee.incomeTax}</td>
                    <td className="two wide">{employee.netIncome}</td>
                    <td className="two wide">{employee.grossIncome}</td>
                    <td className="two wide">{employee.super}</td>
                </tr>
            )
        }

        return (
            <React.Fragment>
                {this.state.submitEmployeeDetails
                    ?
                    <div>
                        <Header size='medium'>Employees Monthly Payslip Details</Header>
                        <table className="ui striped table">
                            <thead>
                                <tr>
                                    <th className="two wide">Employee Name</th>
                                    <th className="two wide">Pay Peiord</th>
                                    <th className="two wide">Income Tax</th>
                                    <th className="two wide">Net Income</th>
                                    <th className="two wide">Gross Income</th>
                                    <th className="two wide">Super Rate</th>
                                </tr>
                            </thead>
                            <tbody>
                                {employeesPaySlip}
                            </tbody>
                        </table>
                    </div>
                    :
                    <div>
                        <Header size='medium'>Please fill employees detail to get Employees Monthly Payslip</Header>
                        <Form id="employeePayDetailForm">
                            <Form.Field>
                                <label>First Name</label>
                                <input type="text" name="firstName" placeholder='First Name' onChange={event => this.handleChange(event)} />
                                <div style={{ color: 'red' }}>
                                    {this.state.errors.firstName}
                                </div>
                            </Form.Field>

                            <Form.Field>
                                <label>Last Name</label>
                                <input type="text" name="lastName" placeholder='Last Name' onChange={event => this.handleChange(event)} />
                                <div style={{ color: 'red' }}>
                                    {this.state.errors.lastName}
                                </div>
                            </Form.Field>

                            <Form.Field>
                                <label>Annual Salary</label>
                                <input type="number" name="salary" placeholder='Annual Salary' onChange={event => this.handleChange(event)} />
                                <div style={{ color: 'red' }}>
                                    {this.state.errors.salary}
                                </div>
                            </Form.Field>

                            <Form.Field>
                                <label>Super Rate</label>
                                <input type="number" name="superRate" placeholder='Super Rate' onChange={event => this.handleChange(event)} />
                                <div style={{ color: 'red' }}>
                                    {this.state.errors.superRate}
                                </div>
                            </Form.Field>

                            <Form.Field>
                                <label>Pay Start Date</label>
                                <input type="date" name="startDate" placeholder='YYYY/MM/DD' onChange={event => this.handleChange(event)} />
                                <div style={{ color: 'red' }}>
                                    {this.state.errors.startDate}
                                </div>
                            </Form.Field>

                            <Form.Field>
                                <label>Pay End Date</label>
                                <input type="date" name="endDate" placeholder='YYYY/MM/DD' onChange={event => this.handleChange(event)} />
                                <div style={{ color: 'red' }}>
                                    {this.state.errors.endDate}
                                </div>
                            </Form.Field>
                        </Form>
                        <br />
                        <Button onClick={this.addMoreEmployee} className="ui secondary button">Add Employee
                </Button>
                        <Button floated="right" onClick={this.finalSubmit} className="ui green button">Generate Payslips
                </Button>
                    </div>
}                             
            </React.Fragment>
        );
    }
}
