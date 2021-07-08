using EmployeesMVC.DataModel;
using EmployeesMVC.DataModel.Employee;
using EmployeesMVC.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesMVC.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _employeeRestService;
        private readonly IConfiguration _configuration;

        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
            _employeeRestService = new HttpClient();
            _employeeRestService.BaseAddress = new Uri(_configuration.GetSection("ExternalServices").GetSection("EmployeeAPI").Value);
        }
        public async Task<CommandResponse<IEnumerable<Employee>>> GetEmployees()
        {
            var result = new CommandResponse<IEnumerable<Employee>>();

            try
            {
                var response = await _employeeRestService.GetAsync("employees");
                if (response.IsSuccessStatusCode)
                {
                    // Read all of the response and deserialise it into an instace of Employee class
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<CommandResponse<IEnumerable<Employee>>>(content);
                }
            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
