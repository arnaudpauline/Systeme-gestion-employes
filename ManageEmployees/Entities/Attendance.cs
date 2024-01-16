using System;
using System.Collections.Generic;

namespace ManageEmployees.Entities;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime ArrivingDate { get; set; }

    public DateTime? DepartureDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
