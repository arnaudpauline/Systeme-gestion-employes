using ManageEmployees.Dtos.Employee;
using ManageEmployees.Dtos.EmployeeDepartement;

namespace ManageEmployees.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<ReadEmployee> CreateEmployeeAsync(CreateEmployee employee);
        Task CreateEmployeeDepartementAsync(int EmployeeId, int DepartementId);

        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <exception cref="System.Exception">
        /// Echec de mise à jour d'un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}
        /// or
        /// Echec de mise à jour d'un employé : Il existe déjà un employé avec ce nom {employee.Name}
        /// </exception>
        Task UpdateEmployeeAsync(int employeeId, UpdateEmployee employee);

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        Task<List<ReadEmployee>> GetEmployees();

        /// <summary>
        /// Gets the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un employé car il n'existe pas : {employeeId}</exception>
        Task<ReadEmployee> GetEmployeeByIdAsync(int employeeId);

        /// <summary>
        /// Gets the employee by name asynchronous.
        /// </summary>
        /// <param name="employeeName">Name of the employee.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un employé car il n'existe pas de nom correspondant : {employeeName}</exception>
        Task<ReadEmployee> GetEmployeeByNameAsync(string employeeName);

        /// <summary>
        /// Deletes the employee by identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <exception cref="System.Exception">
        /// Echec de suppression d'un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}
        /// or
        /// Echec de suppression car ce employé est lié à des employés
        /// </exception>
        Task DeleteEmployeeById(int employeeId);
        Task DeleteEmployeeDepartmentById(int employeedepartmentId);
    }
}
