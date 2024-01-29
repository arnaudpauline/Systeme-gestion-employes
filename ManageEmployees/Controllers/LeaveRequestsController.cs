using ManageEmployees.Dtos.LeaveRequest;
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
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ILeaveRequestService _leaverequestService;

        public LeaveRequestsController(ILeaveRequestService leaverequestService)
        {
            _leaverequestService = leaverequestService;
        }

        [HttpGet("GetLR/")]
        public async Task<ActionResult<List<ReadLeaveRequest>>> GetLeaveRequestsAsync()
        {
            var leaverequests = await _leaverequestService.GetLeaveRequests();
            return Ok(leaverequests);
        }


        [HttpGet("GetLRById/{id}")]
        public async Task<ActionResult<ReadLeaveRequest>> GetLeaveRequestByIdAsync(int id)
        {
            if (id < 1)
                BadRequest($"Echec de recupération d'un departement : Il n'existe pas de departement avec cet Id {id}");

            try
            {
                var leaverequest = await _leaverequestService.GetLeaveRequestByIdAsync(id);
                return Ok(leaverequest);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        // POST api/<LeaveRequestsController>
        [HttpPost("PostLeaveReq/")]
        public async Task<ActionResult<ReadLeaveRequest>> Post([FromBody] CreateLeaveRequest leaverequest)
        {
            if (leaverequest == null || string.IsNullOrWhiteSpace(leaverequest.EmployeeId.ToString())
                || string.IsNullOrWhiteSpace(leaverequest.RequestDate) || string.IsNullOrWhiteSpace(leaverequest.StartDate)
                || string.IsNullOrWhiteSpace(leaverequest.EndDate) || string.IsNullOrWhiteSpace(leaverequest.LeaveRequestStatusId.ToString()))
            {
                return BadRequest("Echec de création d'une demande de congé : les informations sont null ou vides");
            }

            try
            {
                var leaverequestCreated = await _leaverequestService.CreateLeaveRequestAsync(leaverequest);
                return Ok(leaverequestCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("PutLeaveReq/{id}")]
        public async Task<ActionResult> UpdateLeaveRequestAsync(int id,[FromBody] UpdateLeaveRequest leaverequest)
        {
            if (leaverequest == null || string.IsNullOrWhiteSpace(leaverequest.StartDate)
                || string.IsNullOrWhiteSpace(leaverequest.EndDate) || string.IsNullOrWhiteSpace(leaverequest.RequestDate)
                || string.IsNullOrWhiteSpace(leaverequest.EmployeeId.ToString()) || string.IsNullOrWhiteSpace(leaverequest.LeaveRequestStatusId.ToString()))
            {
                return BadRequest("Echec de mise jour d'un departement : les informations sont null ou vides");
            }

            try
            {
                await _leaverequestService.UpdateLeaveRequestAsync(id, leaverequest);
                return Ok(new
                {
                    Message = $"Succès de la mise à jour de la demande de congé : {id}",
                }) ;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
