using Examen.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IAttendanceService
    {
        public void Update();
        public void Delete(Student student);
        public Dictionary<Student, AttendanceStatus?> GetAll();
        public void UpdateAttendanceStatus(int studentId, AttendanceStatus newStatus);
        public IEnumerable<Student> StudentToSign();
        public void CreateNewAttendanceRecord();
    }
}
