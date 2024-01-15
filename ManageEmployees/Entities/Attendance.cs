using System;
using System.Collections.Generic;

namespace ManageEmployees.Entities;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime ArrivalTime { get; set; }

    public DateTime? DepartureTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
