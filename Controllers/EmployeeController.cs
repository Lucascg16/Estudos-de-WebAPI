using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/V1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm]EmployeeViewModel employeeView)
        {
            var filePath = Path.Combine("Storage", employeeView.photo.FileName);
            
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            employeeView.photo.CopyTo(fileStream);

            var emplyee = new Employee(employeeView.Name, employeeView.age, filePath);

            _employeeRepository.Add(emplyee);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var employee = _employeeRepository.Get(id);

            var databytes = System.IO.File.ReadAllBytes(employee.photo);

            return File(databytes, "image/png");
        }

        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            var employees = _employeeRepository.Get(pageNumber, pageQuantity);
            return Ok(employees);
        }
    }
}
