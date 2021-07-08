using AutoMapper;
using EmployeesMVC.DataModel.Employee;
using EmployeesMVC.Models;
using EmployeesMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var employeesResponse = await _employeeService.GetEmployees();
            if (employeesResponse.Status != "success")
            {
                return NotFound();
            }

            IEnumerable<EmployeeViewModel> employees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employeesResponse.Data);

            return View(employees);
        }
    }
}
