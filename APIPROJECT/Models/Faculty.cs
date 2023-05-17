namespace APIPROJECT.Models
{
    public class Faculty
    {

        public int FacultyId { get; set; }
        public string FacultyName { get;set; }
        public string FacultyDesignation { get;set; }
        public string FacultyDept { get; set; }
        public string Facultymail { get; set; } 
        public string FacultySalary { get; set; }
        public Course Course { get; set; }

    }
}
