using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RazorMvc.Models;
using RazorMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorMvc.Controllers
{
    [Route("employee/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbService employeeService;
        //private readonly IHubContext<MessageHub> hubContext;

        public EmployeeController(EmployeeDbService employeeService)
        {
            this.employeeService = employeeService;
        }

        // GET: employee/<EmployeeController>
        [EnableCors("Policy1")]
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employeeService.GetEmployees();
        }

        // GET employee/<EmployeeController>/5
       [EnableCors("Policy1")]
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return employeeService.GetEmployeeById(id);
        }

        // POST employee/<EmployeeController>
        [EnableCors("Policy1")]
        [HttpPost]
        public void Post([FromBody] Employee employee)
        {
            var newEmployee = employeeService.AddEmployee(employee);
            //hubContext.Clients.All.SendAsync("AddMember", newMember.Name, newMember.Id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            employeeService.RemoveEmployee(id);
        }
    }
}
