using ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AuthenticationService
    {
        private const string JsonFilePath = @"C:\Users\user\source\repos\AttendanceProject\ApplicationCore\Domain\Dummy.json";
        private AttendanceRecord _rootObject;

        public AuthenticationService()
        {
            _rootObject = LoadJsonData();
        }

        private AttendanceRecord LoadJsonData()
        {
            string jsonString = File.ReadAllText(JsonFilePath);
            return JsonSerializer.Deserialize<AttendanceRecord>(jsonString) ?? new AttendanceRecord();
        }
        public void UpdatePwd(string newPwd)
        {
            // First, load the entire JSON structure into _rootObject
            _rootObject = LoadJsonData();

            // Then, update the password
            _rootObject.Auth.pwd = newPwd;

            // Finally, serialize and save the whole _rootObject back to the file
            var options = new JsonSerializerOptions { WriteIndented = true };
            try
            {
                string jsonString = JsonSerializer.Serialize(_rootObject, options);
                File.WriteAllText(JsonFilePath, jsonString);
            }
            catch (IOException e)
            {
                Console.WriteLine($"An I/O error occurred: '{e}'");
            }
        }
    }
}
