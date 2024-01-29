using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;
using ManageEmployees.Services.Contracts;
using ManageEmployees.Services.Implementations;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet("GetDep/")]
        public async Task<ActionResult<List<ReadDepartment>>> GetDepartmentsAsync()
        {
            var departments = await _departementService.GetDepartments();
            return Ok(departments);
        }

        [HttpGet("GetDepByName/{name}")]
        public async Task<ActionResult<ReadDepartment>> GetDepartmentByIdAsync(string name)
        {
            
            if (string.IsNullOrWhiteSpace(name))
                BadRequest("Echec de recupération d'un departement : le nom du departement est invalide");

            try
            {
                var department = await _departementService.GetDepartmentByNameAsync(name);
                return Ok(department);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetDepById/{id}")]
        public async Task<ActionResult<ReadDepartment>> GetDepartmentByIdAsync(int id)
        {
            if (id < 1)
                BadRequest($"Echec de recupération d'un departement : Il n'existe pas de departement avec cet Id {id}");

            try
            {
                var department = await _departementService.GetDepartmentByIdAsync(id);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        // POST api/<DepartmentsController>
        [HttpPost("PostDep/")]
        public async Task<ActionResult<ReadDepartment>> Post([FromBody] CreateDepartment department)
        {
            if (department == null || string.IsNullOrWhiteSpace(department.Name)
                || string.IsNullOrWhiteSpace(department.Address) || string.IsNullOrWhiteSpace(department.Description))
            {
                return BadRequest("Echec de création d'un departement : les informations sont null ou vides");
            }

            try
            {
                var departmentCreated = await _departementService.CreateDepartmentAsync(department);
                return Ok(departmentCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("PutDep/{id}")]
        public async Task<ActionResult> UpdateDepartementAsync(int id,[FromBody] UpdateDepartment department)
        {
            if (department == null || string.IsNullOrWhiteSpace(department.Name)
                || string.IsNullOrWhiteSpace(department.Address) || string.IsNullOrWhiteSpace(department.Description))
            {
                return BadRequest("Echec de mise jour d'un departement : les informations sont null ou vides");
            }

            try
            {
                await _departementService.UpdateDepartmentAsync(id, department);
                return Ok(new
                {
                    Message = $"Succès de la mise à jour du departement : {id}",
                }) ;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete("DeleteDep/{id}")]
        public async Task<ActionResult> DeleteDepartementByIdAsync(int id)
        {
            if (id < 1)
                BadRequest($"Echec de suppression d'un departement : Il n'existe pas de departement avec cet Id {id}");

            try
            {
                await _departementService.DeleteDepartmentById(id);
                return Ok(new
                {
                    Message = $"Succès de la suppression du departement : {id}",
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
