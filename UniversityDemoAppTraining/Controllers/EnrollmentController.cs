using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using UniversityDemoAppTraining.Models;
using UniversityDemoAppTraining.Services;

namespace UniversityDemoAppTraining.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly DbMySQLService _DbMySQLSevice;

        private readonly EnrollmentService _enrollmentService;

        public EnrollmentController(DbMySQLService DbMySQLService, EnrollmentService enrollmentService)
        {
            _DbMySQLSevice = DbMySQLService;
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllEnrollment()
        {
            var enrollments = _enrollmentService.GetEnrollmentsAll();
            return Ok(enrollments);
        }

        [HttpGet("{id}")]
        public IActionResult GetEnrollmentbyId(int id)
        {
            var enrollment = _enrollmentService.GetByID(id);
            return enrollment == null ? Ok() : Ok(enrollment);
        }

        [HttpPost]
        [Route("AddEnrollment")]
        public IActionResult AddEnrollment([FromBody] Enrollment enrollment)
        {
            try
            {
                enrollment.enrollment_id = 0;
                bool success = _enrollmentService.AddEnrollment(enrollment);
                if (!success)
                {
                    return BadRequest("Invalid student_id, course_id, or teacher_id.");
                }
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateEnrollment")]
        public IActionResult UpdateEnrolment(int id, [FromBody] Enrollment enrollment)
        {
            bool isUpdated = _enrollmentService.UpdateEnrollment(id, enrollment.student_id, enrollment.course_id, enrollment.teacher_id, enrollment.grade);
            return isUpdated ? Ok(enrollment) : NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteEnrollment(int id)
        {
            bool isDeleted = _enrollmentService.DeleteEnrollment(id);
            return isDeleted ? Ok() : NotFound();
        }
    }
}
