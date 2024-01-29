using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface IAttendanceRepository
    {
        Task<List<Attendance>> GetAttendancesAsync();

        Task<Attendance> GetAttendanceByIdAsync(int attendanceId);

        Task<Attendance> GetAttendanceByIdWithIncludeAsync(int attendanceId);


        Task<Attendance> CreateAttendanceAsync(Attendance attendanceToCreate);

    }
}
