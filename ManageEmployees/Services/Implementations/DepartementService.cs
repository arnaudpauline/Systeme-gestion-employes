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

        public async Task<ReadDepartment> CreateDepartmentAsync(string departmentName)
        {
            var departmentGet = await _departementRepository.GetDepartmentByNameAsync(departmentName);
            if(departmentGet is not null)
            {
                throw new Exception($"Echec de création d'un département : Il existe déjà un département avec ce nom {departmentName}");
            }

            Department department = new ()
            {
                Name = departmentName,
            };

           var departmentCreated =  await _departementRepository.CreateDepartmentAsync(department);

            return new ReadDepartment
            {
                Id = departmentCreated.DepartmentId,
                Name = departmentCreated.Name,
            };
        }
    }
}
