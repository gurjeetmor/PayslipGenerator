# Generate one or more Employees Monthly Payslips

## <a href = "https://gurjeetmor.github.io/RockPaperScissorsGame/" > Live Here </a>

# Project Overview

## Solution Structure

* The solution functionality divided into two separate projects. 

- GeneratePayslip project manages the whole application. This project maintains the:
	- Controllers to manage the connectivity between front-end and back-end (PayslipGeneratorController)
	- Models contain the entities include in the project (Employee, EmployeePayslip, TaxRule)
	- Services responsible for receiving data from controller, process and return result back to controller (CoreServiceProvider, EmployeePayslipGenerator, EmployeePayslipCalculator and other interfaces).
	- React Components handle the front-end UI part and send the data to backend and provide the result to user through attractive UI (GeneratePayslipFromFile, Home, GenerateSingleEmployeePayslip, Home, NavMenu).
- GeneratePayslipTest project runs the several Unit Test cases and perform the several validation checks.

## Assumptions:
* Employee details file need to be in CSV format to get the expected output. But I used the validation checks when user select the file through browse button so user will get the message for required steps to get the result.
* The StartDate and EndDate month needs to be same as the Program is used to generate the Monthly Payslip.
* Moreover, I used the several validation checks for front-end such as Form all fields are required, number field contain the number value, superRate value within provided range.

## Solution Dependencies
* I use the few libraries to deliver the specialised solution such as:
- CSVHelper (http://joshclose.github.io/CsvHelper/) Used for processing CSV files. Installed directly from solution NuGet Package Manager.
- Semantic UI React (https://react.semantic-ui.com/) Used to design the much more attractive and interactive UI like Form, Table to display result. Installed using NuGet Package Manager Console npm add semantic-ui-css 
- MSTest Test Framework used to create and run the test cases. Installed directly from solution NuGet Package Manager.

## Building the application
* Need Visual Studio Community Edition or Visual Studio Code.
* Need to Install Node.Js in system as application uses the NuGet Package Manager or NPM to install the required packages and libraries.
* Clone the project in VS and open the solution in VS.
* Open the solution folder in Command Prompt and Run the commands npm install, npm start.
* Or Open the solution within Visual Studio and restore NuGet packages.
* Build the solution and run in any choice of browser.

## For Testing the application
* In Visual Studio just run all tests from Test menu or directly run the file under GeneratePayslipTest project.

## Executing the application
* Browse the application in any browser. There are three main Link Home, Using CSV, Using Form.
- Home page have Dropdown menu with option to submit employee details like through browse button select the CSV file or use the Form method to enter the employee information in form. Depending on the selection control transfer to the selected page.
- If user select the CSV file upload mode then, browse link will display where user selectthe file and after submission will get the processed employees payslip data in table format. User can select the file with any number of records but should be CSV file with matched fields. 
	- Input should be in this order (first_name, last_name, annual_salary, super_rate (%), month_start_date-month_end_date): like (Gurjeet,Kaur,60050,3%,01 March – 31 March)
- On the other hand, if user select the Form option then a Form will display on UI screen which allow the user to enter employee’s information. This form hastwo buttons, one to submit the entered employee details and second button allow user toenter the more than one employee details by adding employees and save the state as anArray of objects. When click on submit button all array value will be sending to backend for processing and will get the payslip details for all submitted employees. 


<img src = "img/payslipGenerator.gif" alt = "" />