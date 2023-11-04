using Examen.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.Domain
{
    public class AttendanceRecord
    {
        [JsonPropertyName("AttendanceRecords")]
        public List<Attendance> AttendanceLog { get; set; } = new List<Attendance>();
        [JsonPropertyName("Auth")]
        public Authentication Auth { get; set; }
    }
}
