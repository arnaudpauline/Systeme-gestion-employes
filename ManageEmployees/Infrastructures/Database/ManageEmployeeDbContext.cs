using System;
using System.Collections.Generic;
using ManageEmployees.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Infrastructures.Database;

public partial class ManageEmployeeDbContext : DbContext
{
    public ManageEmployeeDbContext()
    {
    }

    public ManageEmployeeDbContext(DbContextOptions<ManageEmployeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<LeaveRequestStatus> LeaveRequestStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ManageEmployees;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69261CD69B048B");

            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BEDCE7C312B");

            entity.HasIndex(e => e.Name, "UQ__Departme__737584F6A965A5AF").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1118544C9F");

            entity.HasIndex(e => e.Email, "UK_Employees_Email").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeDepartment>(entity =>
        {
            entity.HasKey(e => e.EmployeeDepartmentId).HasName("PK__Employee__4634B46257FE52B0");

            entity.HasIndex(e => new { e.EmployeeId, e.DepartmentId }, "UK_EmployeeDepartments_EmployeeId_DepartmentId").IsUnique();

            entity.HasOne(d => d.Department).WithMany(p => p.EmployeeDepartments).HasForeignKey(d => d.DepartmentId);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeDepartments).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestId).HasName("PK__LeaveReq__609421EEF3ED6C0A");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.RequestDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequests).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.LeaveRequestStatus).WithMany(p => p.LeaveRequests).HasForeignKey(d => d.LeaveRequestStatusId);
        });

        modelBuilder.Entity<LeaveRequestStatus>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestStatusId).HasName("PK__LeaveReq__14C2CED18E73B96C");

            entity.ToTable("LeaveRequestStatus");

            entity.HasIndex(e => e.Status, "UQ__LeaveReq__3A15923FA8920D5C").IsUnique();

            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
