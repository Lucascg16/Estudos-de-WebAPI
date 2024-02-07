using WebApi.Domain.DTO;

namespace WebApi.Domain.Model
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);

        List<EmployeeDTO> Get(int pageNumber, int pageQuantity);
        Employee? Get(int id);
    }
}
