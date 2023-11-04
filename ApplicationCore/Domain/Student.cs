using ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="This field is required ! ")]
        public string? firstName { get; set; }
        [Required(ErrorMessage = "This field is required ! ")]
        public string? lastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? birthDate { get; set; }
        public AttendanceStatus? AttendanceStatus { get; set; } = null;
        public Formation Formation { get; set; }
        
    }
}
