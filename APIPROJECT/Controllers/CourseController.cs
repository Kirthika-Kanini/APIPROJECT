using APIPROJECT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPROJECT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly FacultyCourseContext _context;

        public CourseController(FacultyCourseContext context)
        {
            _context = context;
        }


        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> Get()
        {
            return await _context.Courses.Include(x => x.Faculties).ToListAsync();
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Get(int id)
        {
            Course c = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == id);
            return c;
        }


        // POST api/<CoursesController>
        [HttpPost]
        public async Task<ActionResult<Course>> Post(Course c)
        {
            _context.Courses.Add(c);
            _context.SaveChanges();
            return Ok(c);
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Course cou)
        {

            _context.Entry(cou).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(cou);
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {


            var cou = await _context.Courses.FindAsync(id);


            _context.Courses.Remove(cou);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("count/{courseName}")]
        public async Task<ActionResult<int>> GetCourseCount(string courseName)
        {
            var course = await _context.Courses.Include(x => x.Faculties).FirstOrDefaultAsync(c => c.CourseName == courseName);

            if (course == null)
            {
                return NotFound();
            }
            return Ok("Faculty Count under " +courseName+" Course are:"+ course.Faculties.Count);
            
        }
        // GET: api/Course/requirefaculty/{courseName}
        [HttpGet("requirefaculty/{courseName}")]
        public async Task<ActionResult<string>> DoesCourseRequireFaculty(string courseName)
        {
            Course course = await _context.Courses.Include(x => x.Faculties).FirstOrDefaultAsync(c => c.CourseName == courseName);

            if (course == null)
            {
                return NotFound();
            }

            bool facultyRequired = course.Faculties.Count == 0;
            string description = facultyRequired ? "Faculty Required" : "Faculty Not Required";
            return description;
        }



    }
}
