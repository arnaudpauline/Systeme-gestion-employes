using ManageEmployees.Dtos.LeaveRequest;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Services.Contracts;
using System.Globalization;

namespace ManageEmployees.Services.Implementations
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaverequestRepository;
        public LeaveRequestService(ILeaveRequestRepository leaverequestRepository)
        {
            _leaverequestRepository = leaverequestRepository;
        }

        /// <summary>
        /// Gets the LeaveRequest.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReadLeaveRequest>> GetLeaveRequests()
        {
            var leaveRequests = await _leaverequestRepository.GetLeaveRequestsAsync();

            return leaveRequests.Select(lr => new ReadLeaveRequest
            {
                //Id = lr.Id,
                EmployeeId = lr.EmployeeId,
                StartDate = lr.StartDate.ToString("yyyy-MM-dd"),
                EndDate = lr.EndDate.ToString("yyyy-MM-dd"),
                RequestDate = lr.RequestDate.ToString("yyyy-MM-dd"),
                LeaveRequestStatusId = lr.LeaveRequestStatusId
            }).ToList();
        }

        /// <summary>
        /// Gets the department by identifier asynchronous.
        /// </summary>
        /// <param name="LeaveRequestid">The LeaveRequest identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'une delande de congé car il n'existe pas : {LeaveRequestId}</exception>
        public async Task<ReadLeaveRequest> GetLeaveRequestByIdAsync(int leaverequestId)
        {
            var leaverequest = await _leaverequestRepository.GetLeaveRequestByIdAsync(leaverequestId);

            if (leaverequest == null)
            {
                throw new Exception($"Echec de récupération des informations d'une demande de congé car il n'existe pas : {leaverequestId}");
            }

            return new ReadLeaveRequest()
            {
                //Id = leaverequest.Id,
                EmployeeId = leaverequest.EmployeeId,
                StartDate = leaverequest.StartDate.ToString("yyyy-MM-dd"),
                EndDate = leaverequest.EndDate.ToString("yyyy-MM-dd"),
                RequestDate = leaverequest.RequestDate.ToString("yyyy-MM-dd"),
                LeaveRequestStatusId = leaverequest.LeaveRequestStatusId,
            };
        }


        /// <summary>
        /// Updates the LeaveRequest asynchronous.
        /// </summary>
        /// <param name="LeaveRequestId">The LeaveRequest identifier.</param>
        /// <param name="LeaveRequest">The v.</param>
        /// <exception cref="System.Exception">
        /// Echec de mise à jour d'une demande de congé : Il n'existe aucune demande avec cet identifiant : {LeaveRequestId}
        /// or
        /// Echec de mise à jour d'une demande de congé : Il existe déjà une demande a cette date pour cette id {LeaveRequest.employeeId, LeaveRequest.Date}
        /// </exception>
        public async Task UpdateLeaveRequestAsync(int leaverequestId, UpdateLeaveRequest leaverequest)
        {
            var leaverequestGet = await _leaverequestRepository.GetLeaveRequestByIdAsync(leaverequestId)
        ?? throw new Exception($"Echec de mise à jour d'une demande : Il n'existe aucune demande avec cet identifiant : {leaverequestId}");

            var startDate = DateTime.Parse(leaverequest.StartDate);

            var existingLeaveRequest = await _leaverequestRepository.GetLeaveRequestByDateIdAsync(startDate.ToString(), leaverequest.EmployeeId);

            if (existingLeaveRequest != null && existingLeaveRequest.LeaveRequestId != leaverequestId)
            {
                throw new Exception($"Echec de mise à jour d'une demande de congé : Il existe déjà une demande avec cet employé {leaverequest.EmployeeId} à cette date de début {leaverequest.StartDate}");
            }

            leaverequestGet.EmployeeId = leaverequest.EmployeeId;
            leaverequestGet.RequestDate = DateTime.Parse(leaverequest.RequestDate);
            leaverequestGet.StartDate = startDate;
            leaverequestGet.EndDate = DateTime.Parse(leaverequest.EndDate);
            leaverequestGet.LeaveRequestStatusId = leaverequest.LeaveRequestStatusId;


            await _leaverequestRepository.UpdateLeaveRequestAsync(leaverequestGet);

        }



        /// <summary>
        /// Creates the LeaveRequest asynchronous.
        /// </summary>
        /// <param name="LeaveRequest">The LeaveRequest.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de création d'une demande : Il existe déjà une demande avec cet employé et à la meme date {LeaveRequest.startdate,LeaveRequest.epmloyeeId}</exception>
        public async Task<ReadLeaveRequest> CreateLeaveRequestAsync(CreateLeaveRequest leaverequest)
        {
            var existingLeaveRequest = await _leaverequestRepository.GetLeaveRequestByDateIdAsync(leaverequest.StartDate, leaverequest.EmployeeId);
            if (existingLeaveRequest != null)
            {
                throw new Exception($"Echec de création d'une demande de congé : Il existe déjà une demande avec cet employé {leaverequest.EmployeeId} à cette date {leaverequest.StartDate}");
            }

            var leaveRequestToCreate = new LeaveRequest()
            {
                EmployeeId = leaverequest.EmployeeId,
                StartDate = DateTime.ParseExact(leaverequest.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(leaverequest.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                RequestDate = DateTime.ParseExact(leaverequest.RequestDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                LeaveRequestStatusId = leaverequest.LeaveRequestStatusId,
            };

            var createdLeaveRequest = await _leaverequestRepository.CreateLeaveRequestAsync(leaveRequestToCreate);

            return new ReadLeaveRequest()
            {
                //Id = createdLeaveRequest.,
                EmployeeId = createdLeaveRequest.EmployeeId,
                StartDate = createdLeaveRequest.StartDate.ToString("yyyy-MM-dd"),
                EndDate = createdLeaveRequest.EndDate.ToString("yyyy-MM-dd"),
                RequestDate = createdLeaveRequest.RequestDate.ToString("yyyy-MM-dd"),
                LeaveRequestStatusId = createdLeaveRequest.LeaveRequestStatusId,
            };
        }


        public async Task<ReadLeaveRequest> GetLeaveRequestByDateIdAsync(string leaverequestDate, int leaverequestEmployeeId)
        {
            var leaverequest = await _leaverequestRepository.GetLeaveRequestByDateIdAsync(leaverequestDate, leaverequestEmployeeId);

            if (leaverequest == null)
            {
                throw new Exception($"Echec de récupération des informations d'une demande de congé pour la date {leaverequestDate} et l'employé ID {leaverequestEmployeeId}");
            }

            return new ReadLeaveRequest()
            {
                //Id = leaverequest.Id,
                EmployeeId = leaverequest.EmployeeId,
                StartDate = leaverequest.StartDate.ToString("yyyy-MM-dd"),
                EndDate = leaverequest.EndDate.ToString("yyyy-MM-dd"),
                RequestDate = leaverequest.RequestDate.ToString("yyyy-MM-dd"),
                LeaveRequestStatusId = leaverequest.LeaveRequestStatusId,
            };
        }

    }
}
