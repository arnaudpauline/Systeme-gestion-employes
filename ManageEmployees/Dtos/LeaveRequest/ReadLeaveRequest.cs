namespace ManageEmployees.Dtos.LeaveRequest
{
    public class ReadLeaveRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public string RequestDate { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int LeaveRequestStatusId { get; set; }
    }
}
