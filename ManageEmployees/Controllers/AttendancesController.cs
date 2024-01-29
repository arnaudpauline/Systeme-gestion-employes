using ManageEmployees.Dtos.Attendance;
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
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("GetAtt/")]
        public async Task<ActionResult<List<ReadAttendance>>> GetAttendancesAsync()
        {
            var attendances = await _attendanceService.GetAttendances();
            return Ok(attendances);
        }


        [HttpGet("GetAttById/{id}")]
        public async Task<ActionResult<ReadAttendance>> GetAttendanceByIdAsync(int id)
        {
            if (id < 1)
                BadRequest($"Echec de recupération d'une présence : Il n'existe pas de précense avec cet Id {id}");

            try
            {
                var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
                return Ok(attendance);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        // POST api/<AttendancesController>
        [HttpPost("PostAtt/")]
        public async Task<ActionResult<ReadAttendance>> Post([FromBody] CreateAttendance attendance)
        {
            if (attendance == null || string.IsNullOrWhiteSpace(attendance.ArrivingDate.ToString())
                || string.IsNullOrWhiteSpace(attendance.EmployeeId.ToString()))
            {
                return BadRequest("Echec de création d'une présence : les informations sont null ou vides");
            }

            try
            {
                var attendanceCreated = await _attendanceService.CreateAttendanceAsync(attendance);
                return Ok(attendanceCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
