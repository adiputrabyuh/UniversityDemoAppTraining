using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
//using SimpleUniversityDemo.Models;
using SimpleUniversityDemo.Services;
using UniversityDemoAppTraining.Services;
namespace SimpleUniversityDemo.Controllers

{
    [ApiController]
    [Route("Controller")]
    public class StudentController : ControllerBase
    {
        private readonly DbMySQLService _mySQLDB;
        private readonly StudentService _studentService;
        public StudentController(DbMySQLService mySQLDB, StudentService studentService)
        {
            _mySQLDB = mySQLDB;
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