namespace ManageEmployees.Dtos.Attendance
{
    public class ReadAttendance
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string ArrivingDate { get; set; }

        public string DepartureDate { get; set; }
    }
}
