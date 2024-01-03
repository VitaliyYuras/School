using Microsoft.AspNetCore.Mvc;
using School.Data.Models;
using School.Services;

namespace School.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentService.GetStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(Guid id)
        {
            var student = _studentService.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            _studentService.AddStudent(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(Guid id, Student updatedStudent)
        {
            _studentService.UpdateStudent(id, updatedStudent);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(Guid id)
        {
            _studentService.DeleteStudent(id);
            return NoContent();
        }
    }
}
