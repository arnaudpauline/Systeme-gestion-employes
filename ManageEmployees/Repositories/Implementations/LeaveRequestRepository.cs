using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories.Implementations
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ManageEmployeeDbContext _dbContext;

        public LeaveRequestRepository(ManageEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsAsync()
        {
            return await _dbContext.LeaveRequests.ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestByIdAsync(int leaverequestId)
        {
            return await _dbContext.LeaveRequests.FirstOrDefaultAsync(x => x.LeaveRequestId == leaverequestId);
        }

        public async Task<LeaveRequest> GetLeaveRequestByDateIdAsync(string leaverequestDate, int leavereuqtestEmployeeId)
        {
            return await _dbContext.LeaveRequests.FirstOrDefaultAsync(x => x.StartDate.ToString() == leaverequestDate && x.EmployeeId == leavereuqtestEmployeeId);
        }

        public async Task UpdateLeaveRequestAsync(LeaveRequest leaverequestToUpdate)
        {
            _dbContext.LeaveRequests.Update(leaverequestToUpdate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest leaverequestToCreate)
        {
            await _dbContext.LeaveRequests.AddAsync(leaverequestToCreate);
            await _dbContext.SaveChangesAsync();

            return leaverequestToCreate;
        }
    }
}
