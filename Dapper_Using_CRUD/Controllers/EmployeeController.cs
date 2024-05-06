using Dapper_Using_CRUD.Models;
using Dapper_Using_CRUD.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dapper_Using_CRUD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeesRepo _employeesRepo;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeesRepo employeesRepo)
        {
            _logger = logger;
            _employeesRepo = employeesRepo;
        }

        [HttpGet(Name = "GetEmployee")]
        public async Task<IActionResult> GetEmployee()
        {
            var employee = await _employeesRepo.GetEmployee();
            if (!employee.Any())
            {
                return NotFound("Records not found");
            }
            return Ok(employee);
        }

        [HttpGet("GetByIdEmployee/{Id}")]
        public async Task<IActionResult> GetByIdEmployee(int Id)
        {
            var employee = await _employeesRepo.GetByIdEmployee(Id);
            if (!employee.Any())
            {
                return NotFound("Record not found");
            }
            return Ok(employee);
        }


        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(Employees employees)
        {
            var employee = await _employeesRepo.Add(employees);
            return Ok(employee);
        }

        [HttpPut("UpdateEmployee/{Id}")]
        public async Task<IActionResult> UpdateEmployee(int Id, Employees employees)
        {
            var employee = await _employeesRepo.Update(Id, employees);
            return Ok(employee);
        }

        [HttpDelete("DeleteEmployee/{Id}")]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            await _employeesRepo.Delete(Id);
            return Ok("Record deleted successfully");
        }
    }
}
