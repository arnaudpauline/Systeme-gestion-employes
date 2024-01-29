using ManageEmployees.Dtos.Department;

namespace ManageEmployees.Services.Contracts
{
    public interface IDepartementService
    {
        Task<ReadDepartment> CreateDepartmentAsync(CreateDepartment department);

        /// <summary>
        /// Updates the department asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <param name="department">The department.</param>
        /// <exception cref="System.Exception">
        /// Echec de mise à jour d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}
        /// or
        /// Echec de mise à jour d'un département : Il existe déjà un département avec ce nom {department.Name}
        /// </exception>
        Task UpdateDepartmentAsync(int departmentId, UpdateDepartment department);

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        Task<List<ReadDepartment>> GetDepartments();

        /// <summary>
        /// Gets the department by identifier asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un département car il n'existe pas : {departmentId}</exception>
        Task<ReadDepartment> GetDepartmentByIdAsync(int departmentId);

        /// <summary>
        /// Gets the department by name asynchronous.
        /// </summary>
        /// <param name="departmentName">Name of the department.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un département car il n'existe pas de nom correspondant : {departmentName}</exception>
        Task<ReadDepartment> GetDepartmentByNameAsync(string departmentName);

        /// <summary>
        /// Deletes the department by identifier.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <exception cref="System.Exception">
        /// Echec de suppression d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}
        /// or
        /// Echec de suppression car ce departement est lié à des employés
        /// </exception>
        Task DeleteDepartmentById(int departmentId);
    }
}
