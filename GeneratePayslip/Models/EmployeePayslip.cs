using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratePayslip.Models
{
    public class EmployeePayslip
    {
        public decimal GrossIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public string Name { get; set; }
        public decimal NetIncome { get; set; }
        public string PayPeriod { get; set; }
        public decimal Super { get; set; }
    }
}
