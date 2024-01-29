using WebApi.Model;

namespace WebApi.Infra
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConnectionContext _Context = new();

        public void Add(Employee employee)
        {
            _Context.Employees.Add(employee);
            _Context.SaveChanges();
        }

        public List<Employee> Get()
        {
            return _Context.Employees.ToList();
        }
    }
}
