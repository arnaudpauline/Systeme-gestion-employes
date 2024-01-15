using ManageEmployees.Dtos.Department;
using ManageEmployees.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartementService _departementService;

        public DepartmentsController(IDepartementService departementService)
        {
            _departementService = departementService;
        }

        // POST api/<DepartmentsController>
        [HttpPost]
        public async Task<ActionResult<ReadDepartment>> Post([FromBody] string departmentName)
        {
            if(string.IsNullOrWhiteSpace(departmentName))
            {
                return BadRequest("Echec de création d'un departement : Le nom du department est null ou vide");
            }

            try
            {
                var department = await _departementService.CreateDepartmentAsync(departmentName);

                return Ok(department);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
