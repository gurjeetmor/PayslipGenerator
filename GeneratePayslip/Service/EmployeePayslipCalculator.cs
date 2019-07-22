using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratePayslip.Models;

namespace GeneratePayslip.Service
{
    public class EmployeePayslipCalculator:IEmployeePayslipCalculator
    {
        private const int monthsInAYear = 12;

        public List<TaxRule> Rules { get; }

        //Default constructor to load the static rules
        public EmployeePayslipCalculator() : this(TaxRule.LoadRules())
        {

        }

        //Call once when the first time generator created and then support the provided custom rules
        public EmployeePayslipCalculator(List<TaxRule> rulesToApply)
        {
            if (rulesToApply != null && rulesToApply.Count > 0)
            {
                Rules = rulesToApply;
            }
        }
        //Calculate the pay data for multiple employees
        public async Task<List<EmployeePayslip>> Calculate(List<Employee> employees)
        {
            List<EmployeePayslip> payslips = new List<EmployeePayslip>();

            foreach (var emp in employees)
            {
                payslips.Add(Calculate(emp));
            }
            await Task.Run(() => {
                return payslips;
            });
            return payslips;
        }

        //Calculate the single empployee pay at once 
        //Result sends to multiple employees pay Calculation method
        public EmployeePayslip Calculate(Employee employee)
        {
            foreach (TaxRule rule in Rules)
            {
                if (employee.Salary >= rule.MinTaxIncome)
                {
                    //Use to check the Tax rule without MaxTaxIncome
                    if (rule.MaxTaxIncome.HasValue)
                    {
                        if (employee.Salary <= rule.MaxTaxIncome.Value)
                            return CalculateIncome(employee, rule);
                    }
                    else
                        return CalculateIncome(employee, rule);
                }
            }

            return null;
        }

        /*
            This method is used to calculate the rounded dollar value 
            as requirement >=50 cents round up to the next dollar increment, otherwise round down.

            Method keep separated so that the RoundingMechanism is consistent throughout our calculations.
        */
        public static decimal SalaryRound(decimal val)
        {
            return Math.Round(val, MidpointRounding.AwayFromZero);
        }


        /*
          All require payslip generator calculations performed in CalculateIncome
          • pay period = per calendar month            • gross income = annual salary / 12 months            • income tax = based on the taxRule            • net income = gross income - income tax            • super = gross income x super rate 
         */

        private EmployeePayslip CalculateIncome(Employee employee, TaxRule rule)
        {
            EmployeePayslip employeePayslip = new EmployeePayslip();
            employeePayslip.Name = $"{employee.FirstName} {employee.LastName}";
            employeePayslip.PayPeriod = $"{employee.StartDate.Value.ToString("dd MMMM yyyy")}-{employee.EndDate.Value.ToString("dd MMMM yyyy")}";
            employeePayslip.GrossIncome = SalaryRound(employee.Salary / monthsInAYear);
            employeePayslip.Super = SalaryRound(employeePayslip.GrossIncome * employee.SuperRate.Value / 100);
            employeePayslip.IncomeTax = SalaryRound((rule.BaseTaxAmount + (employee.Salary - rule.MinTaxIncome) * rule.ExcessAmount) /
                               monthsInAYear);
            employeePayslip.NetIncome = employeePayslip.GrossIncome - employeePayslip.IncomeTax;
            return employeePayslip;
        }
    }
}
