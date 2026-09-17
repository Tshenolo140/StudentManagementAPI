using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "John", Surname = "Smith", Email = "john.smith@example.com", Course = "Information Technology", Year = 1, StudentNumber = "ST1001" },
            new Student { Id = 2, Name = "Mary", Surname = "Jones", Email = "mary.jones@example.com", Course = "Computer Science", Year = 2, StudentNumber = "ST1002" },
            new Student { Id = 3, Name = "Peter", Surname = "Molefe", Email = "peter.molefe@example.com", Course = "Information Technology", Year = 1, StudentNumber = "ST1003" },
            new Student { Id = 4, Name = "Sarah", Surname = "Mokoena", Email = "sarah.mokoena@example.com", Course = "Software Development", Year = 3, StudentNumber = "ST1004" },
            new Student { Id = 5, Name = "David", Surname = "Naidoo", Email = "david.naidoo@example.com", Course = "Information Technology", Year = 2, StudentNumber = "ST1005" }
        };

        // GET: /api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(students);
        }

        // GET: /api/students/{id}
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        // POST: /api/students
        [HttpPost]
        public IActionResult CreateStudent([FromBody] Student newStudent)
        {
            if (newStudent == null)
                return BadRequest();

            int newId = students.Any() ? students.Max(s => s.Id) + 1 : 1;
            newStudent.Id = newId;
            students.Add(newStudent);

            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
        }

        // PUT: /api/students/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            if (updatedStudent == null || id != updatedStudent.Id)
                return BadRequest();

            var existingStudent = students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
                return NotFound();

            existingStudent.Name = updatedStudent.Name;
            existingStudent.Surname = updatedStudent.Surname;
            existingStudent.Email = updatedStudent.Email;
            existingStudent.Course = updatedStudent.Course;
            existingStudent.Year = updatedStudent.Year;
            existingStudent.StudentNumber = updatedStudent.StudentNumber;

            return Ok(existingStudent);
        }

        // DELETE: /api/students/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            students.Remove(student);
            return NoContent();
        }
    }
}