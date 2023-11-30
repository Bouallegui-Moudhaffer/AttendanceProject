using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System.Text.Json;
using System.Linq.Expressions;
using ApplicationCore.Domain;

namespace Examen.ApplicationCore.Services
{
    public class AttendanceService : IAttendanceService
    {
        private AttendanceRecord _attendanceRecordsRoot;
        private IEnumerable<Student> _students;
        private const string JsonFilePath = @"C:\Users\user\source\repos\AttendanceProject\ApplicationCore\Domain\Dummy.json";

        public AttendanceService()
        {
            _attendanceRecordsRoot = LoadJsonData();

            if (!_attendanceRecordsRoot.AttendanceLog.Any() ||
                _attendanceRecordsRoot.AttendanceLog.Last().recordDate?.Date != DateTime.Now.Date)
            {
                CreateNewAttendanceRecord();
            }

            _students = _attendanceRecordsRoot?.AttendanceLog.LastOrDefault()?.StudentsData ?? Enumerable.Empty<Student>();
        }

        private AttendanceRecord LoadJsonData()
        {
            string jsonString = File.ReadAllText(JsonFilePath);
            return JsonSerializer.Deserialize<AttendanceRecord>(jsonString) ?? new AttendanceRecord();
        }

        public void CreateNewAttendanceRecord()
        {
            var lastRecord = _attendanceRecordsRoot.AttendanceLog.LastOrDefault();
            var newRecord = new Attendance
            {
                recordDate = DateTime.Now,
                StudentsData = lastRecord?.StudentsData.Select(s => new Student
                {
                    Id = s.Id,
                    firstName = s.firstName,
                    lastName = s.lastName,
                    birthDate = s.birthDate,
                    AttendanceStatus = null, // Reset AttendanceStatus
                    Formation = s.Formation
                }).ToList() ?? new List<Student>()
            };
            _attendanceRecordsRoot.AttendanceLog.Add(newRecord);
            Update();
        }

            public void Update()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            // Serialize the whole AttendanceRecord object instead of just _students
            File.WriteAllText(JsonFilePath, JsonSerializer.Serialize(_attendanceRecordsRoot, options));
        }

        public void Delete(Student student)
        {
            _students = _students.Where(s => s.Id != student.Id);
            // Update the AttendanceRecord with the new students list
            _attendanceRecordsRoot.AttendanceLog.LastOrDefault().StudentsData = _students.ToList();
            Update();
        }

        public Dictionary<Student, AttendanceStatus?> GetAll()
        {
            return _students.ToDictionary(student => student, student => student.AttendanceStatus);
        }

        public void UpdateAttendanceStatus(int studentId, AttendanceStatus newStatus)
        {
            var student = _students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                student.AttendanceStatus = newStatus;
                _attendanceRecordsRoot.AttendanceLog.LastOrDefault().StudentsData = _students.ToList();
                Update();
            }
            else
            {
                Console.WriteLine($"Student with ID {studentId} not found.");
            }
        }

        public IEnumerable<Student> StudentToSign()
        {
            return _students.Where(e => e.Formation.Equals(1));
        }
    }
}
