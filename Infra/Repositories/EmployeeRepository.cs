using WebApi.Domain.DTO;
using WebApi.Domain.Model;

namespace WebApi.Infra.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConnectionContext _Context = new();

        public void Add(Employee employee)
        {
            _Context.Employees.Add(employee);
            _Context.SaveChanges();
        }

        public List<EmployeeDTO> Get(int pageNumber, int pageQuantity)
        {
            var employee = _Context.Employees.Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Select(p => new EmployeeDTO
                {
                    Id = p.id,
                    Name = p.name,
                    Photo = p.photo
                }).ToList();
            return employee;
        }

        public Employee? Get(int id)
        {
            return _Context.Employees.Find(id);
        }
    }
}
