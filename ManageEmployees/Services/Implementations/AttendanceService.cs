using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;
using System.Globalization;

namespace ManageEmployees.Services.Implementations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        /// <summary>
        /// Gets the attendances.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReadAttendance>> GetAttendances()
        {
            var attendances = await _attendanceRepository.GetAttendancesAsync();

            List<ReadAttendance> readAttendances = new List<ReadAttendance>();

            foreach (var attendance in attendances)
            {
                readAttendances.Add(new ReadAttendance()
                {
                    Id = attendance.AttendanceId,
                    ArrivingDate = attendance.ArrivingDate.ToString(),
                    DepartureDate = attendance.DepartureDate.ToString(),
                    EmployeeId = attendance.EmployeeId,
                });
            }

            return readAttendances;
        }

        /// <summary>
        /// Gets the attendance by identifier asynchronous.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'une précense car il n'existe pas : {attendanceId}</exception>
        public async Task<ReadAttendance> GetAttendanceByIdAsync(int attendanceId)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIdAsync(attendanceId);

            if (attendance is null)
                throw new Exception($"Echec de recupération des informations d'une présence car il n'existe pas : {attendanceId}");

            return new ReadAttendance()
            {
                Id = attendance.AttendanceId,
                ArrivingDate = attendance.ArrivingDate.ToString(),
                DepartureDate = attendance.DepartureDate.ToString(),
                EmployeeId = attendance.EmployeeId,
            };
        }


        /// <summary>
        /// Creates the attendance asynchronous.
        /// </summary>
        /// <param name="attendance">The attendance.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de création d'une présence</exception>
        public async Task<ReadAttendance> CreateAttendanceAsync(CreateAttendance attendance)
        {
            try
            {
                if (attendance == null || string.IsNullOrWhiteSpace(attendance.EmployeeId.ToString()))
                {
                    throw new ArgumentException("Les informations de présence sont manquantes ou invalides.");
                }

                DateTime? departureDateTime = null;

                if (!string.IsNullOrWhiteSpace(attendance.DepartureDate))
                {
                    if (!DateTime.TryParseExact(attendance.DepartureDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime departureTemp))
                    {
                        throw new ArgumentException("Format de date de départ invalide.");
                    }
                    departureDateTime = departureTemp;
                }

                var attendanceToCreate = new Attendance()
                {
                    ArrivingDate = DateTime.Parse(attendance.ArrivingDate),
                    DepartureDate = departureDateTime,
                    EmployeeId = attendance.EmployeeId,
                };

                var attendanceCreated = await _attendanceRepository.CreateAttendanceAsync(attendanceToCreate);

                return new ReadAttendance()
                {
                    Id = attendanceCreated.AttendanceId,
                    ArrivingDate = attendanceCreated.ArrivingDate.ToString(),
                    EmployeeId = attendanceCreated.EmployeeId,
                    DepartureDate = attendanceCreated.DepartureDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                };
            }
            catch (Exception ex)
            {
                // Loguez l'exception pour le débogage
                Console.WriteLine($"Erreur lors de la création d'une présence : {ex.Message}");
                throw; // Renvoie l'exception pour une gestion ultérieure
            }
            /*var attendanceTocreate = new Attendance()
            {
                AttendanceId = attendance.EmployeeId,
                ArrivingDate = attendance.ArrivingDate,
                DepartureDate = attendance.DepartureDate,
                EmployeeId = attendance.EmployeeId,
            };

            var attendanceCreated = await _attendanceRepository.CreateAttendanceAsync(attendanceTocreate);

            return new ReadAttendance()
            {
                Id = attendanceCreated.AttendanceId,
                ArrivingDate = attendanceCreated.ArrivingDate,
                EmployeeId = attendanceCreated.EmployeeId,
                DepartureDate = attendanceCreated.DepartureDate,
            };*/
        }
    }
}
