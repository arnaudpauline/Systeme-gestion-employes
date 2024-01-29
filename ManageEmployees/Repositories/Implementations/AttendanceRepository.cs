using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories.Implementations
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ManageEmployeeDbContext _dbContext;

        public AttendanceRepository(ManageEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Attendance>> GetAttendancesAsync()
        {
            return await _dbContext.Attendances.ToListAsync();
        }

        public async Task<Attendance> GetAttendanceByIdAsync(int attendanceId)
        {
            return await _dbContext.Attendances.FirstOrDefaultAsync(x => x.AttendanceId == attendanceId);
        }

        public async Task<Attendance> GetAttendanceByIdWithIncludeAsync(int attendanceId)
        {
            return await _dbContext.Attendances
                .FirstOrDefaultAsync(x => x.AttendanceId == attendanceId);
        }


        public async Task<Attendance> CreateAttendanceAsync(Attendance attendanceToCreate)
        {
            await _dbContext.Attendances.AddAsync(attendanceToCreate);
            await _dbContext.SaveChangesAsync();

            return attendanceToCreate;
        }
    }
}
