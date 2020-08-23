using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Interfaces;
using EmployeeManagement.Contracts.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManagementService _employeeManagementService;

        public EmployeeController(IEmployeeManagementService employeeManagementService)
        {
            _employeeManagementService = employeeManagementService;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _employeeManagementService.GetAll();
        }

        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return _employeeManagementService.GetById(id);
        }

        [HttpPost]
        [Authorize]
        public void Post([FromBody] Employee employee)
        {
            _employeeManagementService.Update(employee);
        }

        [HttpPut]
        [Authorize]
        public void Put([FromBody] Employee employee)
        {
            _employeeManagementService.Update(employee);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public string Delete(int id)
        {
             return _employeeManagementService.Delete(id);
        }
    }
}
