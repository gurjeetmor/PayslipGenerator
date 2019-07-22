using System;
using System.Collections.Generic;
using GeneratePayslip.Models;
using GeneratePayslip.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//This class validate the models fields value
namespace GeneratePayslipTests
{
    [TestClass]
    public class GeneratePayslipTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
          "First Name must be provided")]
        public void NameValidationTest()
        {
            EmployeeModelForTesting emp = new EmployeeModelForTesting("", "", 7000, 9.0m, "2018-01-01", "2018-01-30", "test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Super Rate value must be between 0 and 50 both inclusive")]
        public void RateNegativeValidationTest()
        {
            EmployeeModelForTesting emp = new EmployeeModelForTesting("Gurjeet", "Kaur", 7000, -9.0m, "2018-01-01", "2018-01-30", "test");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Super Rate value must be between 0 and 50 both inclusive")]
        public void RateOutOfRangeValidationTest()
        {
            EmployeeModelForTesting emp = new EmployeeModelForTesting("Gurjeet", "Kaur", 7000, 51, "2018-01-01", "2018-01-30", "test");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException),
            "Start Date format should be of type yyyy-mm-dd")]
        public void DateInvalidFormatValidationTest()
        {
            EmployeeModelForTesting emp = new EmployeeModelForTesting("Gurjeet", "Kaur", 7000, 9.0m, "2018-01-01", "2018  -30", "test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Salary is a positive value")]
        public void SalaryValidationTest()
        {
            EmployeeModelForTesting emp = new EmployeeModelForTesting("Gurjeet", "Kaur", -7000, 9.0m, "2018-01-01", "2018-01-30", "test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Start Date must be provided and in the format yyyy-mm-dd")]
        public void DateMissingValidationTest()
        {
            EmployeeModelForTesting emp = new EmployeeModelForTesting("Gurjeet", "Kaur", 7000, 9.0m, "", "2018-01-01", "test");
        }     
    }
}
