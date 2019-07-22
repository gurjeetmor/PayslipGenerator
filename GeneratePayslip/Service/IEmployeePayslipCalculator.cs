using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratePayslip.Models;

namespace GeneratePayslip.Service
{
    //Interface use to calculate employees payslip data from input employee information
    //Implemented in EmployeePayslipCalculator
    public interface IEmployeePayslipCalculator
    {
        EmployeePayslip Calculate(Employee employee);
        Task<List<EmployeePayslip>> Calculate(List<Employee> employee);
    }
}
