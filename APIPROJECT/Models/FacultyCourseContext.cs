using Microsoft.EntityFrameworkCore;

namespace APIPROJECT.Models
{
    public class FacultyCourseContext:DbContext
    {

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }  
        public FacultyCourseContext(DbContextOptions<FacultyCourseContext> options) : base(options)
        {

        }
    }
}
