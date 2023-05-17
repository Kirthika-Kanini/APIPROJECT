namespace APIPROJECT.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string ? CourseName { get;set; }
        public string ? CourseDescription { get;set; }
        public string ? CourseType { get; set; }  

        public string ? CourseDuration { get; set; }

        public int ? CourseEnrollment { get; set; }

        public ICollection<Faculty>? Faculties { get; set; }

    }
}
