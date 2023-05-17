using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPROJECT.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIPROJECT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly FacultyCourseContext _context;

        public FacultiesController(FacultyCourseContext context)
        {
            _context = context;
        }

        // GET: api/Faculties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculties()
        {
          if (_context.Faculties == null)
          {
              return NotFound();
          }
            return await _context.Faculties.Include(x => x.Course).ToListAsync();
        }

        // GET: api/Faculties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> GetFaculty(int id)
        {
          if (_context.Faculties == null)
          {
              return NotFound();
          }
            var faculty = await _context.Faculties.FindAsync(id);

            if (faculty == null)
            {
                return NotFound();
            }

            return faculty;
        }

        // PUT: api/Faculties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaculty(int id, Faculty faculty)
        {
            if (id != faculty.FacultyId)
            {
                return BadRequest();
            }

            var fac = await _context.Courses.FindAsync(faculty.Course.CourseId);
            faculty.Course = fac;

            _context.Entry(faculty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Faculties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Faculty>> PostFaculty(Faculty faculty)
        {
          if (_context.Faculties == null)
          {
              return Problem("Entity set 'FacultyCourseContext.Faculties'  is null.");
          }

            var fac = await _context.Courses.FindAsync(faculty.Course.CourseId);
            faculty.Course = fac;
            _context.Faculties.Add(faculty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFaculty", new { id = faculty.FacultyId }, faculty);
        }



        // POST: api/Faculties/courseid
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{courseId}")]
        public async Task<ActionResult<Faculty>> PostStudent(int courseId, Faculty faculty)
        {
            if (_context.Faculties == null)
            {
                return Problem("Entity set 'CourseStudentContext.Students' is null.");
            }

            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }

            var numStudentsEnrolled = await _context.Faculties.CountAsync(s => s.Course.CourseId == courseId);
            if (numStudentsEnrolled >= course.CourseEnrollment)
            {
                return BadRequest("You cannot choose this course to handel");
            }
            var fac = await _context.Courses.FindAsync(faculty.Course.CourseId);
            faculty.Course = fac;
            faculty.Course = course;
            _context.Faculties.Add(faculty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFaculty", new { id = faculty.FacultyId }, faculty);
        }
        // DELETE: api/Faculties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculty(int id)
        {
            if (_context.Faculties == null)
            {
                return NotFound();
            }
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }

            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Faculty>>> SearchEmployees(string searchTerm)
        {
            if (_context.Faculties == null)
            {
                return NotFound();
            }

            var faculties = await _context.Faculties
                .Include(x => x.Course)
                .Where(f => f.FacultyName.Contains(searchTerm) || f.Course.CourseName.Contains(searchTerm))
                .ToListAsync();

            if (faculties == null || faculties.Count == 0)
            {
                return Ok("Faculty not found");
            }
          

            return faculties;
        }
        private bool FacultyExists(int id)
        {
            return (_context.Faculties?.Any(e => e.FacultyId == id)).GetValueOrDefault();
        }
    }
}
