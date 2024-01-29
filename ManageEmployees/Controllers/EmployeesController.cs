using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;
using ManageEmployees.Services.Contracts;
using ManageEmployees.Services.Implementations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService departementService)
        {
            _employeeService = departementService;
        }

        [HttpGet("GetEmployee/")]
        public async Task<ActionResult<List<ReadEmployee>>> GetEmployeesAsync()
        {
            var employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("GetEmplByName/{name}")]
        public async Task<ActionResult<ReadEmployee>> GetEmployeeByIdAsync(string name)
        {
            
            if (string.IsNullOrWhiteSpace(name))
                BadRequest("Echec de recupération d'un employé : le nom de l'empoyé est invalide");

            try
            {
                var employee = await _employeeService.GetEmployeeByNameAsync(name);
                return Ok(employee);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetEmplById/{id}")]
        public async Task<ActionResult<ReadEmployee>> GetEmployeeByIdAsync(int id)
        {
            if (id < 1)
                BadRequest($"Echec de recupération d'un employé : Il n'existe pas d'employé avec cet Id {id}");

            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        // POST api/<EmployeesController>
        [HttpPost("PostEmployee/")]
        public async Task<ActionResult<ReadEmployee>> Post([FromBody] CreateEmployee employee)
        {
            if (employee == null || string.IsNullOrWhiteSpace(employee.LastName)
                || string.IsNullOrWhiteSpace(employee.LastName) || string.IsNullOrWhiteSpace(employee.FirstName))
            {
                return BadRequest("Echec de création d'un employé : les informations sont null ou vides");
            }

            if(DateTime.TryParseExact(employee.BirthDate,"yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime dt) == false)
            {
                return BadRequest("Echec de création d'un employé : la date d'anniversaire ne correspond pas au format 'YYYY-MM-DD'");
            }

            if (Regex.IsMatch(employee.PhoneNumber, @"[^0-9]") || employee.PhoneNumber.Length != 10)
            {
                return BadRequest("Echec de création d'un eployé : le numéro de téléphone ne peut contenir que 10 chiffres (sans lettrees et caractères spéciaux)");
            }

            if (!Regex.IsMatch(employee.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                return BadRequest("Échec de création d'un employé : l'adresse e-mail n'est pas au format valide.");
            }

            try
            {
                var employeeCreated = await _employeeService.CreateEmployeeAsync(employee);
                return Ok(employeeCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("PutEmployee/{id}")]
        public async Task<ActionResult> UpdateEmployeeAsync(int id,[FromBody] UpdateEmployee employee)
        {
            if (employee == null || string.IsNullOrWhiteSpace(employee.LastName)
                || string.IsNullOrWhiteSpace(employee.LastName) || string.IsNullOrWhiteSpace(employee.FirstName))
            {
                return BadRequest("Echec de mise jour d'un employé : les informations sont null ou vides");
            }

            try
            {
                await _employeeService.UpdateEmployeeAsync(id, employee);
                return Ok(new
                {
                    Message = $"Succès de la mise à jour du employé : {id}",
                }) ;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<ActionResult> DeleteEmployeeByIdAsync(int id)
        {
            if (id < 1)
                BadRequest($"Echec de suppression d'un employé : Il n'existe pas d'employé avec cet Id {id}");

            try
            {
                await _employeeService.DeleteEmployeeById(id);
                return Ok(new
                {
                    Message = $"Succès de la suppression d'un employé : {id}",
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/<EmployeesDepartementController>
        [HttpPut("employeeId/{employeeId}/departments/{departmentId}")]
        public async Task<ActionResult> AddDepartmentToEmployee(int employeeId, int departmentId)
        {
            if (employeeId < 0)
            {
                return BadRequest("Echec de d'ajout d'un département à l'employé : le format de l'ID est invalide");
            }
            if (departmentId < 0)
            {
                return BadRequest("Echec de d'ajout d'un département à l'employé : le format de l'ID est invalide");
            }

            try
            {
                await _employeeService.CreateEmployeeDepartementAsync(employeeId, departmentId);
                return Ok("Département d'ID " + departmentId + " ajouté avec succès à l'employé d'ID " + employeeId);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("DeleteEmployeeDepartment/{id}")]
        public async Task<ActionResult> DeleteEmployeeDepartmentByIdAsync(int id)
        {
            if (id < 1)
                BadRequest($"Echec de suppression d'un employé sur un département : Il n'existe pas d'employé sur ce département avec cet Id {id}");

            try
            {
                await _employeeService.DeleteEmployeeDepartmentById(id);
                return Ok(new
                {
                    Message = $"Succès de la suppression d'un employé à un départment : {id}",
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
