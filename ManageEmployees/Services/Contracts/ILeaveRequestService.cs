using ManageEmployees.Dtos.LeaveRequest;

namespace ManageEmployees.Services.Contracts
{
    public interface ILeaveRequestService
    {
        Task<ReadLeaveRequest> CreateLeaveRequestAsync(CreateLeaveRequest leaverequest);


        Task UpdateLeaveRequestAsync(int leaverequestId, UpdateLeaveRequest leaverequest);

        /// <summary>
        /// Gets the LeaveRequest.
        /// </summary>
        /// <returns></returns>
        Task<List<ReadLeaveRequest>> GetLeaveRequests();

        Task<ReadLeaveRequest> GetLeaveRequestByIdAsync(int leaverequestId);

        Task<ReadLeaveRequest> GetLeaveRequestByDateIdAsync(string leaverequestDate, int leaverequestEmployeeId);

    }
}
