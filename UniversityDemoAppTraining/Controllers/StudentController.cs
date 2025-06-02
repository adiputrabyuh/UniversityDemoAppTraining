using Microsoft.AspNetCore.Mvc;
using UniversityDemoAppTraining.Models;
using UniversityDemoAppTraining.Services;
namespace UniversityDemoAppTraining.Controllers

{
    [ApiController]
    [Route("Controller")]
    public class StudentController : ControllerBase
    {
        private readonly DbMySQLService _DbMySQLSevice;
        private readonly StudentService _studentService;
        public StudentController(DbMySQLService DbMySQLService, StudentService studentService)
        {
            _DbMySQLSevice = DbMySQLService;
            _studentService = studentService;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var students = _studentService.GetStudentsAll();
            return Ok(students);
        }
    }
}