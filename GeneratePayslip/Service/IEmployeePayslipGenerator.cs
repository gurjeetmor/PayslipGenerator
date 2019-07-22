using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratePayslip.Models;
using Microsoft.AspNetCore.Http;

namespace GeneratePayslip.Service
{
    //Interface use to process input CSV employee information
    //Implemented in EmployeePayslipGenerator
    public interface IEmployeePayslipGenerator
    {
        Task<List<EmployeePayslip>> Process(List<Employee> totalEmployeeRecordsLoaded);
        
    }
}
