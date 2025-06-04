using Microsoft.AspNetCore.Mvc;
using UniversityDemoAppTraining.Models;
using UniversityDemoAppTraining.Services;
namespace UniversityDemoAppTraining.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly DbMySQLService _DbMySQLSevice;

        private readonly StudentService _studentService;

        public StudentController(DbMySQLService DbMySQLService, StudentService studentService)
        {
            _DbMySQLSevice = DbMySQLService;
            _studentService = studentService;
        }

        // GET: api/Student/GetAll
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var students = _studentService.GetStudentsAll();
            return Ok(students);
        }

        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int student_id)
        {
            var student = _studentService.GetByID(student_id);
            if (student == null)
                return Ok();
            return Ok(student);
        }

        // POST: api/Student/AddStudent
        [HttpPost]
        [Route("AddStudent")]
        public IActionResult AddStudent([FromBody] Student student)
        {
            try
            {
                student.student_id = 0;
                _studentService.AddStudent(student);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Student/UpdateStudent
        [HttpPut]
        [Route("UpdateByID")]
        public IActionResult UpdateStudent(int student_id, [FromBody] Student student)
        {
            bool isUpdated = _studentService.UpdateStudent(student_id, student.first_name, student.last_name, student.email);
            if (!isUpdated)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // DELETE: api/Student/DeleteByID
        [HttpDelete]
        [Route("DeletebyID")]
        public IActionResult DeleteStudent(int student_id)
        {
            bool isDeleted = _studentService.DeleteStudent(student_id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok();
        }


    }
}