using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services.Implementations
{
    public class DepartementService : IDepartementService
    {
        private readonly IDepartementRepository _departementRepository;
        public DepartementService(IDepartementRepository departementRepository)
        {
            _departementRepository = departementRepository;
        }

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReadDepartment>> GetDepartments()
        {
            var departments = await _departementRepository.GetDepartmentsAsync();

            List<ReadDepartment> readDepartments = new List<ReadDepartment>();

            foreach (var department in departments)
            {
                readDepartments.Add(new ReadDepartment()
                {
                    Id = department.DepartmentId,
                    Name = department.Name,
                });
            }

            return readDepartments;
        }

        /// <summary>
        /// Gets the department by identifier asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un département car il n'existe pas : {departmentId}</exception>
        public async Task<ReadDepartment> GetDepartmentByIdAsync(int departmentId)
        {
            var department = await _departementRepository.GetDepartmentByIdAsync(departmentId);

            if (department is null)
                throw new Exception($"Echec de recupération des informations d'un département car il n'existe pas : {departmentId}");

            return new ReadDepartment()
            {
                Id = department.DepartmentId,
                Name = department.Name,
            };
        }

        /// <summary>
        /// Gets the department by name asynchronous.
        /// </summary>
        /// <param name="departmentName">Name of the department.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un département car il n'existe pas de nom correspondant : {departmentName}</exception>
        public async Task<ReadDepartment> GetDepartmentByNameAsync(string departmentName)
        {
            var department = await _departementRepository.GetDepartmentByNameAsync(departmentName);

            if (department is null)
                throw new Exception($"Echec de recupération des informations d'un département car il n'existe pas de nom correspondant : {departmentName}");

            return new ReadDepartment()
            {
                Id = department.DepartmentId,
                Name = department.Name,
            };
        }

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
        public async Task UpdateDepartmentAsync(int departmentId, UpdateDepartment department)
        {
            var departmentGet = await _departementRepository.GetDepartmentByIdAsync(departmentId)
                ?? throw new Exception($"Echec de mise à jour d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}");

            var departmentGetByName = await _departementRepository.GetDepartmentByNameAsync(department.Name);
            if (departmentGetByName is not null && departmentId != departmentGetByName.DepartmentId)
            {
                throw new Exception($"Echec de mise à jour d'un département : Il existe déjà un département avec ce nom {department.Name}");
            }

            departmentGet.Name = department.Name;
            departmentGet.Description = department.Description;
            departmentGet.Address = department.Address;

            await _departementRepository.UpdateDepartmentAsync(departmentGet);

        }

        /// <summary>
        /// Deletes the department by identifier.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <exception cref="System.Exception">
        /// Echec de suppression d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}
        /// or
        /// Echec de suppression car ce departement est lié à des employés
        /// </exception>
        public async Task DeleteDepartmentById(int departmentId)
        {
            var departmentGet = await _departementRepository.GetDepartmentByIdWithIncludeAsync(departmentId)
              ?? throw new Exception($"Echec de suppression d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}");

            if (departmentGet.EmployeeDepartments.Any())
            {
                throw new Exception("Echec de suppression car ce departement est lié à des employés");
            }

            await _departementRepository.DeleteDepartmentByIdAsync(departmentId);
        }

        /// <summary>
        /// Creates the department asynchronous.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de création d'un département : Il existe déjà un département avec ce nom {department.Name}</exception>
        public async Task<ReadDepartment> CreateDepartmentAsync(CreateDepartment department)
        {
            var departmentGet = await _departementRepository.GetDepartmentByNameAsync(department.Name);
            if (departmentGet is not null)
            {
                throw new Exception($"Echec de création d'un département : Il existe déjà un département avec ce nom {department.Name}");
            }

            var departementTocreate = new Department()
            {
                Name = department.Name,
                Description = department.Description,
                Address = department.Address,
            };

            var departmentCreated = await _departementRepository.CreateDepartmentAsync(departementTocreate);

            return new ReadDepartment()
            {
                Id = departmentCreated.DepartmentId,
                Name = departmentCreated.Name,
            };
        }
    }
}
