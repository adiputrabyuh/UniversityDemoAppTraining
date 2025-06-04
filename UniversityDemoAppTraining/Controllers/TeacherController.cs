using Microsoft.AspNetCore.Mvc;
using UniversityDemoAppTraining.Models;
using UniversityDemoAppTraining.Services;
namespace UniversityDemoAppTraining.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly DbMySQLService _DbMySQLSevice;

        private readonly TeacherService _teacherService;

        public TeacherController(DbMySQLService DbMySQLService, TeacherService teacherService)
        {
            _DbMySQLSevice = DbMySQLService;
            _teacherService = teacherService;
        }

        // GET: api/Teacher/GetAll
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var teachers = _teacherService.GetTeachersAll();
            return Ok(teachers);
        }

        // GET: api/Teacher/{id}
        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = _teacherService.GetByID(id);
            if (teacher == null)
                return Ok();
            return Ok(teacher);
        }

        // POST: api/Teacher/AddTeacher
        [HttpPost]
        [Route("AddTeacher")]
        public IActionResult AddTeacher([FromBody] Teacher teacher)
        {
            try
            {
                teacher.id = 0;
                _teacherService.AddTeacher(teacher);
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Teacher/UpdateTeacher
        [HttpPut]
        [Route("UpdateByID")]
        public IActionResult UpdateTeacher(int id, [FromBody] Teacher teacher)
        {
            bool isUpdated = _teacherService.UpdateTeacher(id, teacher.first_name, teacher.last_name, teacher.email);
            if (!isUpdated)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        // DELETE: api/Teacher/DeleteByID
        [HttpDelete]
        [Route("DeletebyID")]
        public IActionResult DeleteTeacher(int id)
        {
            bool isDeleted = _teacherService.DeleteTeacher(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok();
        }


    }
}