using ManageEmployees.Dtos.Attendance;

namespace ManageEmployees.Services.Contracts
{
    public interface IAttendanceService
    {
        Task<ReadAttendance> CreateAttendanceAsync(CreateAttendance attendance);

        /// <summary>
        /// Gets the attendances.
        /// </summary>
        /// <returns></returns>
        Task<List<ReadAttendance>> GetAttendances();

        /// <summary>
        /// Gets the attendance by identifier asynchronous.
        /// </summary>
        /// <param name="departmentId">The attendance identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'une présence car il n'existe pas : {attendanceId}</exception>
        Task<ReadAttendance> GetAttendanceByIdAsync(int attendanceId);


    }
}
