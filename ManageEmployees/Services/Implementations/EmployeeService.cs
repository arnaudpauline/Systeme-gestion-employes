using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartementRepository _departementRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, IDepartementRepository departementRepository)
        {
            _employeeRepository = employeeRepository;
            _departementRepository = departementRepository;
        }

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReadEmployee>> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();

            List<ReadEmployee> readEmployees = new List<ReadEmployee>();

            foreach (var employee in employees)
            {
                readEmployees.Add(new ReadEmployee()
                {
                    Id = employee.EmployeeId,
                    LastName = employee.LastName,
                    FirstName = employee.FirstName,
                    BirthDate = employee.BirthDate.ToString(),
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Position = employee.Position,
                });
            }

            return readEmployees;
        }

        /// <summary>
        /// Gets the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un employé car il n'existe pas : {employeeId}</exception>
        public async Task<ReadEmployee> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee is null)
                throw new Exception($"Echec de recupération des informations d'un employé car il n'existe pas : {employeeId}");

            return new ReadEmployee()
            {
                Id = employee.EmployeeId,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                BirthDate = employee.BirthDate.ToString(),
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position,
            };
        }

        /// <summary>
        /// Gets the employee by name asynchronous.
        /// </summary>
        /// <param name="employeeName">Name of the employee.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un employé car il n'existe pas de nom correspondant : {employeeName}</exception>
        public async Task<ReadEmployee> GetEmployeeByNameAsync(string employeeName)
        {
            var employee = await _employeeRepository.GetEmployeeByNameAsync(employeeName);

            if (employee is null)
                throw new Exception($"Echec de recupération des informations d'un employé car il n'existe pas de nom correspondant : {employeeName}");

            return new ReadEmployee()
            {
                Id = employee.EmployeeId,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                BirthDate = employee.BirthDate.ToString(),
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position,
            };
        }

        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <exception cref="System.Exception">
        /// Echec de mise à jour d'un employée : Il n'existe aucun departement avec cet identifiant : {employeeId}
        /// or
        /// Echec de mise à jour d'un employée
        public async Task UpdateEmployeeAsync(int employeeId, UpdateEmployee employee)
        {
            var employeeGet = await _employeeRepository.GetEmployeeByIdAsync(employeeId)
                ?? throw new Exception($"Echec de mise à jour d'un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}");

            /*var employeeGetByName = await _employeeRepository.GetEmployeeByNameAsync(employee.LastName);
            if (employeeGetByName is not null && employeeId != employeeGetByName.EmployeeId)
            {
                throw new Exception($"Echec de mise à jour d'un employé : Il existe déjà un employé avec ce nom {employee.LastName}");
            }*/

            employeeGet.LastName = employee.LastName;
            employeeGet.FirstName = employee.FirstName;
            employeeGet.BirthDate = DateTime.Parse(employee.BirthDate);
            employeeGet.Email = employee.Email;
            employeeGet.PhoneNumber = employee.PhoneNumber;
            employeeGet.Position = employee.Position;

            await _employeeRepository.UpdateEmployeeAsync(employeeGet);

        }

        /// <summary>
        /// Deletes the employee by identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <exception cref="System.Exception">
        /// Echec de suppression d'un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}
        /// or
        /// Echec de suppression car cet employé est lié à des employés
        /// </exception>
        public async Task DeleteEmployeeById(int employeeId)
        {
            var employeeGet = await _employeeRepository.GetEmployeeByIdWithIncludeAsync(employeeId)
              ?? throw new Exception($"Echec de suppression d'un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}");

            if (employeeGet.EmployeeDepartments.Any())
            {
                foreach (var employeeDepartment in employeeGet.EmployeeDepartments.ToList())
                {
                    await _employeeRepository.DeleteEmployeeDepartmentByIdAsync(employeeDepartment.EmployeeDepartmentId);
                }
                //throw new Exception("Echec de suppression car cet est lié à un/des département/s");
            }

            await _employeeRepository.DeleteEmployeeByIdAsync(employeeId);
        }

        /// <summary>
        /// Creates the employee asynchronous.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de création d'un département : Il existe déjà un employé avec ce nom {employee.Name}</exception>
        public async Task<ReadEmployee> CreateEmployeeAsync(CreateEmployee employee)
        {
            var employeeGet = await _employeeRepository.GetEmployeeByEmailAsync(employee.Email);
            if (employeeGet is not null)
            {
                throw new Exception($"Echec de création d'un employée : Il existe déjà un employé avec cet email {employee.Email}");
            }

            employeeGet = await _employeeRepository.GetEmployeeByPhoneAsync(employee.PhoneNumber);
            if (employeeGet is not null)
            {
                throw new Exception($"Echec de création d'un employée : Il existe déjà un employé avec cee numéro de téléphone {employee.PhoneNumber}");
            }

            var employeeTocreate = new Employee()
            {
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                BirthDate = DateTime.Parse(employee.BirthDate), //employee.BirthDate, //convertir ici la chaine en DateTime ?  
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position,
            };

            var employeeCreated = await _employeeRepository.CreateEmployeeAsync(employeeTocreate);

            return new ReadEmployee()
            {
                Id = employeeCreated.EmployeeId,
                LastName = employeeCreated.LastName,
                FirstName = employeeCreated.FirstName,
                BirthDate = employeeCreated.BirthDate.ToString(),
                Email = employeeCreated.Email,
                PhoneNumber = employeeCreated.PhoneNumber,
                Position = employee.Position,
            };
        }

        public async Task CreateEmployeeDepartementAsync(int employeeId, int departmentId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
            {
                throw new Exception($"Echec d'ajout d'un département à un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}");
            }

            var department = _departementRepository.GetDepartmentByIdAsync(departmentId);
            if (department == null)
            {
                throw new Exception($"Echec d'ajout d'un département à un employé : Il n'existe aucun département avec cet identifiant : {departmentId}");
            }

            var employeeDepartment = new EmployeeDepartment
            {
                EmployeeId = employeeId,
                DepartmentId = departmentId
            };

            await _employeeRepository.CreateEmployeeDepartementAsync(employeeDepartment);
        }


        public async Task DeleteEmployeeDepartmentById(int employeeDepartmentId)
        {
            var employeeDepartmentGet = await _employeeRepository.GetEmployeeDepartmentByIdWithIncludeAsync(employeeDepartmentId)
              ?? throw new Exception($"Echec de suppression d'un employé département : Il n'existe aucun employé département avec cet identifiant : {employeeDepartmentId}");

            /*if (employeeDepartmentGet.EmployeeDepartments.Any())
            {
                throw new Exception("Echec de suppression car ce est lié à des employés départements");
            }*/

            await _employeeRepository.DeleteEmployeeDepartmentByIdAsync(employeeDepartmentId);
        }
    }
}
