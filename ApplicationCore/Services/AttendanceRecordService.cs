using ApplicationCore.Domain;
using Examen.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AttendanceRecordService
    {
        public void AddAttendanceRecord(Attendance newRecord)
        {
            // Deserialize the existing attendance history from the file
            var attendanceHistory = LoadAttendanceHistory();

            // Add the new attendance record
            attendanceHistory.AttendanceLog.Add(newRecord);

            // Serialize the updated history back to the file
            SaveAttendanceHistory(attendanceHistory);
        }

        public AttendanceRecord LoadAttendanceHistory()
        {
            var jsonString = File.ReadAllText("C:\\Users\\user\\source\\repos\\AttendanceProject\\ApplicationCore\\Domain\\Dummy.json");
            var rootObject = JsonSerializer.Deserialize<AttendanceRecord>(jsonString) ?? new AttendanceRecord();
            return new AttendanceRecord { AttendanceLog = rootObject.AttendanceLog };
        }

        public void SaveAttendanceHistory(AttendanceRecord history)
        {
            var jsonString = JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("C:\\Users\\user\\source\\repos\\AttendanceProject\\ApplicationCore\\Domain\\Dummy.json", jsonString);
        }
    }
}
