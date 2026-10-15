using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;

using pract6_zykova.Models;

namespace pract6_zykova.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CourseController : ControllerBase
    {
        private WebApiDatabaseContext _context = new WebApiDatabaseContext();

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? teacher, [FromQuery] int? minDuration)
        {
            var courses = _context.Courses.ToList();

            if (teacher != null)
            {
                courses = courses.Where(c => c.Teacher == teacher).ToList();
            }

            if (minDuration.HasValue)
            {
                courses = courses.Where(c => c.DurationHours >= minDuration.Value).ToList();
            }

            return Ok(courses);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetById),
                new { id = course.Id },
                course
                );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Course updatedCourse)
        {
            if (id != updatedCourse.Id)
            {
                return BadRequest();
            }
            _context.Courses.Update(updatedCourse);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromRoute]int id, [FromBody] CoursesClass patchDoc)
        {
          

            var course = _context.Courses.Find(id);

            if (course == null)
            {
                return NotFound();
            }

            if (patchDoc.Title != null)
            {
                course.Title = patchDoc.Title;
            }

            if (patchDoc.Teacher != null)
            {
                course.Teacher = patchDoc.Teacher;
            }

            if (patchDoc.DurationHours.HasValue)
            {
                course.DurationHours = patchDoc.DurationHours.Value;
            }

            if (patchDoc.Price.HasValue)
            {
                course.Price = patchDoc.Price.Value;
            }

            if (patchDoc.IsActive.HasValue)
            {
                course.IsActive = patchDoc.IsActive.Value;
            }


            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
    