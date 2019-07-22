import React, { Component } from 'react';
import $ from 'jquery';
import { Button, Input, Header, Form } from 'semantic-ui-react';

//Component render the UI to upload the CSV employee detail file
//Display the processed employees payslip information
export class GeneratePayslipFromFile extends Component {
    constructor(props) {
        super(props);

        this.state = {
            selectedFileSrc: '',
            selectedFile: '',
            fileSelected: false,
            employeePayslipsData: [],
            errors: {},
        }       
    };

    //Method used to update the state according to the selected employees detail file
    handleFiles=(event)=> {
        let localSelectedFile = this.state.selectedFile;
        let localFileSrc = this.state.selectedFileSrc;
        localSelectedFile = event.target.files[0];;
        localFileSrc = window.URL.createObjectURL(event.target.files[0]);
        this.setState({
            selectedFile: localSelectedFile,
            selectedFileSrc: localFileSrc,
        })
       
    }

    validateForm() {
        let errors = {}
        let formIsValid = true       
        if (!this.state.selectedFile) {
            formIsValid = false;
            errors['selectedFile'] = '*Please select the employees detail CSV file.';
        }
        else {
            let fileExtension = this.state.selectedFile.name.split('.').pop();
            if (fileExtension !== 'csv') {
                formIsValid = false;
                errors['selectedFile'] = '*Please select CSV file.';
            }
        }
             
        this.setState({
            errors: errors
        });
        return formIsValid
    }

    //Method get the uploaded file and sends the controller method ReadCSVGeneratePayslips
    //using AJAX call to process and generate the payslips.
    fileUploadHandler = (e) => {
        e.preventDefault();
        if (this.validateForm()) {
            let data = new FormData();
            data.append("SelectedFile", this.state.selectedFile);
            data.append("FileUrl", this.state.selectedFileSrc);
            $.ajax({
                url: 'api/PayslipGenerator/ReadCSVGeneratePayslips',
                type: "POST",
                data: data,
                processData: false,
                contentType: false,
                success: function (res) {
                    if (res.success) {
                        this.setState({
                            employeePayslipsData: res.paySlips
                        })
                    } else {
                        console.log("Error in getting employee payslips")
                    }

                }.bind(this),
                error: function (res) {
                    console.log("Error in application call", res)
                }
            });
            this.setState({
                fileSelected: true
            })
        }
    }

    //Render views based on file uploaded or not
    render() {
        let employeesPaySlip = null;
        let employeePaySlipsList = this.state.employeePayslipsData;

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
                {this.state.fileSelected
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
                        <Header size='medium'>Select Employees detail CSV file to generate Employees Monthly Payslip</Header>
                        <Form>
                            <Input id="selectFile" type="file" onChange={this.handleFiles} />
                            <div style={{ color: 'red' }}>
                                {this.state.errors.selectedFile}
                            </div>
                            <br /><br />
                         <Button onClick={this.fileUploadHandler} className="ui green button">Generate Payslip</Button>
                        </Form>
                    </div>
                    }               
            </React.Fragment>
        );
    }
}
