using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        Task<Employee> GetEmployeeByIdWithIncludeAsync(int employeeId);

        Task<Employee> GetEmployeeByNameAsync(string employeeName);

        Task<Employee> GetEmployeeByPhoneAsync(string employeePhone);

        Task<Employee> GetEmployeeByEmailAsync(string employeeEmail);

        Task UpdateEmployeeAsync(Employee employeeToUpdate);

        Task<Employee> CreateEmployeeAsync(Employee employeeToCreate);

        Task<Employee> DeleteEmployeeByIdAsync(int employeeId);

        Task<EmployeeDepartment> CreateEmployeeDepartementAsync(EmployeeDepartment employeeDepartement);

        Task<EmployeeDepartment> DeleteEmployeeDepartmentByIdAsync(int employeeDepartement);

        Task<EmployeeDepartment> GetEmployeeDepartmentByIdWithIncludeAsync(int employeeDepartmentName);

    }
}
