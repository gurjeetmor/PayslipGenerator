using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratePayslip.Service
{
    //Services helps to maitain Dependency Injection Framework
    //Main service use to manage the all other services
    public class CoreServiceProvider
    {
        public static IEmployeePayslipCalculator GetEmployeePayslipCalculator()
        {
            return new EmployeePayslipCalculator();
        }

        public static IEmployeePayslipGenerator GetEmployeePayslipGenerator()
        {
            return new EmployeePayslipGenerator();
        }
    }
}
