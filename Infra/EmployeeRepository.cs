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

        public List<Employee> Get(int pageNumber, int pageQuantity)
        {
            return _Context.Employees.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToList();
        }

        public Employee? Get(int id)
        {
            return _Context.Employees.Find(id);
        }
    }
}
