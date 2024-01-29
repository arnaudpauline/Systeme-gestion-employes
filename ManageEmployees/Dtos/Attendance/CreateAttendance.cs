namespace ManageEmployees.Dtos.Attendance
{
    public class CreateAttendance
    {
        public int EmployeeId { get; set; }

        public string ArrivingDate { get; set; }

        public string DepartureDate { get; set; }
    }
}
