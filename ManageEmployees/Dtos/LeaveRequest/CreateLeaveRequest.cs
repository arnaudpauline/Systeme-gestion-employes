namespace ManageEmployees.Dtos.LeaveRequest
{
    public class CreateLeaveRequest
    {
        public int EmployeeId { get; set; }

        public string RequestDate { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int LeaveRequestStatusId { get; set; }

    }
}
