using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratePayslip.Models
{
    public class EmployeeModelForTesting
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public decimal? SuperRate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Test { get; set; }

        //Use this method to validate the data and check the validation using MSTest Framework
        //All test cases are implemented in GeneratePayslipTest project
        public EmployeeModelForTesting(string fname, string lname, int sal, decimal rate, string startDate, string endDate, string test)
        {
            firstName = fname;
            lastName = lname;
            salary = sal;
            superRate = rate;

            if (String.IsNullOrEmpty(startDate))
                throw new ArgumentException("Start Date must be provided and in the format yyyy-mm-dd");
            else
                StartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (String.IsNullOrEmpty(endDate))
                throw new ArgumentException("End Date must be provided and in the format yyyy-mm-dd");
            else
                EndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public string firstName
        {
            get { return FirstName; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("First Name must be provided");

                FirstName = value;
            }
        }

        public string lastName
        {
            get
            {
                return LastName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Last Name must be provided");

                LastName = value;
            }
        }

        public int salary
        {
            get { return Salary; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Salary is a positive value");

                Salary = value;
            }
        }

        public decimal? superRate
        {
            get { return SuperRate; }
            set
            {
                if (!value.HasValue)
                    throw new ArgumentException("Super Rate must be provided");

                if (value < 0 || value > 50)
                    throw new ArgumentException("Super Rate value must be between 0 and 50 both inclusive");

                SuperRate = value;
            }
        }

        public DateTime? startDate
        {
            get { return StartDate; }
            set
            {
                if (!value.HasValue)
                    throw new ArgumentException("Start Date must be provided");

                StartDate = value;
            }
        }

        public DateTime? endDate
        {
            get { return EndDate; }
            set
            {
                if (!value.HasValue)
                    throw new ArgumentException("End Date must be provided");
                if (DateTime.Compare(startDate.Value, value.Value) >= 0)
                    throw new ArgumentException("End Date has to be greater than Start Date");

                EndDate = value;
            }
        }
    }
}
