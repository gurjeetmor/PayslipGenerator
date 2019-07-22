using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratePayslip.Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public decimal? SuperRate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }       
    }
}
