using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Application.ViewModel;
using WebApi.Domain.DTO;
using WebApi.Domain.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/V1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException();
            _mapper = mapper;
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
            
            if (employee.photo.IsNullOrEmpty()) 
            {
                return Ok("Usuario sem foto");
            }
            
            var databytes = System.IO.File.ReadAllBytes(employee.photo);

            return File(databytes, "image/png");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            var employees = _employeeRepository.Get(pageNumber, pageQuantity);
            return Ok(employees);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult Search(int id)
        {
            var employees = _employeeRepository.Get(id);

            var employeeDTO = _mapper.Map<EmployeeDTO>(employees);

            return Ok(employeeDTO);
        }
    }
}
