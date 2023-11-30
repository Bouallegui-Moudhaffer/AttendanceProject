using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AP.Desktop.ViewModels
{
    public class StudentViewModel : ViewModelBase
    {
        private readonly AttendanceService _attendanceService;
        public ObservableCollection<Student> Students { get; private set; }

        public StudentViewModel()
        {
            _attendanceService = new AttendanceService();
            LoadStudents();
        }

        private void LoadStudents()
        {
            Students = new ObservableCollection<Student>(_attendanceService.GetAll().Keys);
            OnPropertyChanged(nameof(Students));
        }

        public void UpdateAttendanceStatus(Student student, AttendanceStatus status)
        {
            _attendanceService.UpdateAttendanceStatus(student.Id, status);
            // Additional logic to update the student in Students collection
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void SaveChanges()
        {
            _attendanceService.Update(); // This should write changes to the JSON file
        }

    }
}
