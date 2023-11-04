using Examen.ApplicationCore.Domain;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Infrastructure
{
    public class AttendanceStatusConverter : JsonConverter<AttendanceStatus>
    {
        public override AttendanceStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();

            return value.ToLower() switch
            {
                "present" or "p" => AttendanceStatus.Present,
                "absent" or "a" => AttendanceStatus.Absent,
                "excused" or "e" => AttendanceStatus.Excused,
                "late" or "l" => AttendanceStatus.Late,
                _ => throw new JsonException($"Value '{value}' is not a valid attendance status.")
            };
        }

        public override void Write(Utf8JsonWriter writer, AttendanceStatus value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

}