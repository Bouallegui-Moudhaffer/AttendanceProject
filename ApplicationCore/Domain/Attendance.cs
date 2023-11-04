using System.ComponentModel.DataAnnotations;

namespace Examen.ApplicationCore.Domain
{
    public class Attendance
    {
        public IEnumerable<Student> StudentsData { get; set; }
        [DataType(DataType.Date)]
        public DateTime? recordDate { get; set; } = DateTime.Now;
    }
}
