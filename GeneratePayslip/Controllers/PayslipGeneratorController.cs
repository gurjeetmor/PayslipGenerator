using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using GeneratePayslip.Models;
using GeneratePayslip.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace GeneratePayslip.Controllers
{
    //Main controller use to handles the communication between React components and Services
    [Route("api/[controller]")]
    public class PayslipGeneratorController : Controller
    {
        public List<Employee> inputEmployees;
        IEmployeePayslipGenerator payslipGenerator = CoreServiceProvider.GetEmployeePayslipGenerator();

        //This action method use to generate payslips based on submitted employees CSV details file
        [HttpPost("[action]")]
        public async Task<IActionResult> ReadCSVGeneratePayslips()
        {
            IFormFile file = Request.Form.Files[0];
            var reader = new StreamReader(file.OpenReadStream());
            
            try
            {
                if (file.Length > 0)
                {
                    //Use the CsvHelper to read and map the submitted employee CSV file 
                    using (CsvReader csv = new CsvReader(reader))
                    {
                        inputEmployees = csv.GetRecords<Employee>().ToList();
                        //this method process the CSV data
                        var result = await payslipGenerator.Process(inputEmployees);
                        return Json(new { Success = true, PaySlips = result });
                    }
                }
            }
            catch(Exception e)
            {
                return Json(new { Success = false, Message=e });
            }
            return Json(new { Success = false });
        }

        //This method is used to generate the payslips based on submitted employees data through form
        [HttpPost("[action]")]      
        public async Task<IActionResult> DisplayPayslips(string employee)
        {
            //Use the JsonConvert to convert the string JSON data to Employee object
            Employee[] employees = JsonConvert.DeserializeObject<Employee[]>(employee);
            try
            {
                inputEmployees = employees.ToList();
                var result = await payslipGenerator.Process(inputEmployees);
                return Json(new { Success = true, PaySlips = result });
            }
            catch(Exception e)
            {
                return Json(new { Success = false, Message = e });
            }
        }
    }
}