using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataLayer;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IDataProvider _dataProvider;

        public EmployeesController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        // GET: api/Employees
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _dataProvider.GetAllEmployees();
                if (!result.Any())
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        // GET: api/Employees/5
        [HttpGet("GetMeetings/{id}")]
        public IActionResult GetMeetingsForEmployee(int id)
        {
                var result = _dataProvider.GetAllEmployeeMeetings(id);
                if (!result.Any())
                {
                    return NotFound();
                }
                return Ok(result);
        }

        // POST: api/Employees
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
