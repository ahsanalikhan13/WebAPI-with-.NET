using APIcrud.Data;
using APIcrud.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace APIcrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeesController : Controller
    {
        private readonly DbContextData _dbData;

        //constructor to call dbData
        public EmployeesController(DbContextData dbData)
        {
            _dbData = dbData;
        }

        //get all data from table
        [EnableCors("Policy2")]
        [Route("employeeget")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employeeData= await _dbData.Employees.ToListAsync();

            return Ok(employeeData);
        }

        //post data in table
        [EnableCors("Policy2")]
        [Route("employeepost")]
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _dbData.Employees.AddAsync(employeeRequest);
            await _dbData.SaveChangesAsync();
            return Ok(employeeRequest);
        }

        //query the db
        [EnableCors("Policy2")]
        [HttpGet]
        [Route("employeeData{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _dbData.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        //update the existing data in table
        [EnableCors("Policy2")]
        [Route("employeeedit{id:Guid}")]
        [HttpPut]
/*        [Route("{id:Guid}")]
*/        public async Task<IActionResult> updateEmployee([FromRoute] Guid id, Employee updateEmployee)
        {
            var employee = await _dbData.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            employee.Name = updateEmployee.Name;
            employee.Email = updateEmployee.Email;
            employee.Salary = updateEmployee.Salary;
            employee.Department = updateEmployee.Department;
            employee.Phone = updateEmployee.Phone;

            await _dbData.SaveChangesAsync();

            return Ok(employee);
        }

        //delete the item based on id from table
        [EnableCors("Policy2")]
        [Route("employeedelete{id:Guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _dbData.Employees.FindAsync(id);

            if(employee == null) 
            {
                return NotFound();
            }
            _dbData.Employees.Remove(employee);
            await _dbData.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
