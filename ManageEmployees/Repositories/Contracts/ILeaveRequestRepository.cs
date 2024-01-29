using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface ILeaveRequestRepository
    {
        Task<List<LeaveRequest>> GetLeaveRequestsAsync();

        Task<LeaveRequest> GetLeaveRequestByIdAsync(int leaverequestId);

        Task<LeaveRequest> GetLeaveRequestByDateIdAsync(string leaverequestDate, int leaverequestEmployeeId);

        Task UpdateLeaveRequestAsync(LeaveRequest leaverequestToUpdate);

        Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest leaverequestToCreate);

    }
}
