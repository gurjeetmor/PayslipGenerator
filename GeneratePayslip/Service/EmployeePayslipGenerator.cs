using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Threading.Tasks;
using GeneratePayslip.Models;

namespace GeneratePayslip.Service
{
    public class EmployeePayslipGenerator:IEmployeePayslipGenerator
    {
        private List<EmployeePayslip> employeePayslips;
        private readonly IEmployeePayslipCalculator employeePayslipCalculator;

        //Use to initialize the values  
        public EmployeePayslipGenerator()
        {
            employeePayslipCalculator = CoreServiceProvider.GetEmployeePayslipCalculator();
        }

        /*
         Process method use to process the input employees CSV data get from Controller
         and use Calculate method of employeePayslipCalculator service and finally get the 
         proceesed data as Payslips data 
         */

        public async Task<List<EmployeePayslip>> Process(List<Employee> totalEmployeeRecordsLoaded)
        {
            
            if (totalEmployeeRecordsLoaded.Count > 0)
            {
                employeePayslips = await employeePayslipCalculator.Calculate(totalEmployeeRecordsLoaded);
                return employeePayslips;
            }
            return employeePayslips;
        }
    }
}
