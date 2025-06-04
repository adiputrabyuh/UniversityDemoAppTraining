using Microsoft.AspNetCore.Mvc;
using UniversityDemoAppTraining.Models;
using UniversityDemoAppTraining.Services;
namespace UniversityDemoAppTraining.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly DbMySQLService _DbMySQLSevice;

        private readonly CourseService _courseService;

        public CourseController(DbMySQLService DbMySQLService, CourseService courseService)
        {
            _DbMySQLSevice = DbMySQLService;
            _courseService = courseService;
        }

        // GET: api/Student/GetAll
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var courses = _courseService.GetCourseAll();
            return Ok(courses);
        }

        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public IActionResult GetCourseById(int course_id)
        {
            var course = _courseService.GetByID(course_id);
            if (course == null)
                return Ok();
            return Ok(course);
        }

        // POST: api/Student/AddStudent
        [HttpPost]
        [Route("AddCourse")]
        public IActionResult AddCourse([FromBody] Course course)
        {
            try
            {
               course.course_id = 0;
                _courseService.AddCourse(course);
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Student/UpdateStudent
        [HttpPut]
        [Route("UpdateByID")]
        public IActionResult UpdateCourse(int course_id, [FromBody] Course course)
        {
            bool isUpdated = _courseService.UpdateCourse(course_id, course.course_name, course.course_code);
            if (!isUpdated)
            {
                return NotFound();
            }
            return Ok(course);
        }

        // DELETE: api/Student/DeleteByID
        [HttpDelete]
        [Route("DeletebyID")]
        public IActionResult DeleteCourse(int course_id)
        {
            bool isDeleted = _courseService.DeleteCourse(course_id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok();
        }


    }
}