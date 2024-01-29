using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ManageEmployeeDbContext _dbContext;

        public EmployeeRepository(ManageEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
        }

        public async Task<Employee> GetEmployeeByIdWithIncludeAsync(int employeeId)
        {
            return await _dbContext.Employees
                .Include(x => x.EmployeeDepartments)
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
        }

        public async Task<Employee> GetEmployeeByNameAsync(string employeeName)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.LastName == employeeName);
        }

        public async Task<Employee> GetEmployeeByPhoneAsync(string employeePhone)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.PhoneNumber == employeePhone);
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string employeeEmail)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Email == employeeEmail);
        }


        public async Task UpdateEmployeeAsync(Employee employeeToUpdate)
        {
            _dbContext.Employees.Update(employeeToUpdate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employeeToCreate)
        {
            await _dbContext.Employees.AddAsync(employeeToCreate);
            await _dbContext.SaveChangesAsync();

            return employeeToCreate;
        }

        public async Task<Employee> DeleteEmployeeByIdAsync(int employeeId)
        {
            var employeeToDelete = await _dbContext.Employees.FindAsync(employeeId);
            _dbContext.Employees.Remove(employeeToDelete);
            await _dbContext.SaveChangesAsync();
            return employeeToDelete;
        }

        public async Task<EmployeeDepartment> CreateEmployeeDepartementAsync(EmployeeDepartment employeeDepartment)
        {
            await _dbContext.EmployeeDepartments.AddAsync(employeeDepartment);
            await _dbContext.SaveChangesAsync();
            return employeeDepartment;
        }


        public async Task<EmployeeDepartment> DeleteEmployeeDepartmentByIdAsync(int employeedepartmentId)
        {
            var employeedepartmentToDelete = await _dbContext.EmployeeDepartments.FindAsync(employeedepartmentId);
            _dbContext.EmployeeDepartments.Remove(employeedepartmentToDelete);
            await _dbContext.SaveChangesAsync();
            return employeedepartmentToDelete;
        }

        public async Task<EmployeeDepartment> GetEmployeeDepartmentByIdWithIncludeAsync(int employeedepartmentId)
        {
            return await _dbContext.EmployeeDepartments
                .FirstOrDefaultAsync(x => x.EmployeeDepartmentId == employeedepartmentId);
        }
    }
}
