using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
    public class CourseStudentsAssignViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<AssignementStudentViewModel> Students { get; set; }

    }

    public class AssignementStudentViewModel
    {
        public int StudentId { get; set; }
        public string StudentFullName { get; set; }
        public bool IsAssigned { get; set; }
    }
}
