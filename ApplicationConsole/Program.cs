using ApplicationCore.Domain;
using ApplicationCore.Services;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Services;


AttendanceService attendanceService = new AttendanceService();
AuthenticationService authenticationService = new AuthenticationService();

void DisplayStudents()
{
    var students = attendanceService.GetAll();
    foreach (var student in students)
    {
        Console.Write($"Student: {student.Key.firstName} {student.Key.lastName}, Status: {student.Value}");
        if (student.Key.Formation == Formation.FA)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($", Formation: {student.Key.Formation}");
            Console.ResetColor();
        }
        else
        {
            Console.Write($", Formation: {student.Key.Formation}");
        }
        Console.WriteLine();
    }
}

void MarkAbsence()
{
    foreach (var student in attendanceService.GetAll().Keys)
    {
        Console.WriteLine($"Enter Attendance Status for {student.firstName} {student.lastName} (0: Present, 1: Absent, 2: Excused, 3: Late):");
        var input = Console.ReadLine();

        if (Enum.TryParse(input, out AttendanceStatus newStatus) && Enum.IsDefined(typeof(AttendanceStatus), newStatus))
        {
            attendanceService.UpdateAttendanceStatus(student.Id, newStatus);
        }
        else
        {
            Console.WriteLine("Invalid input. Attendance status not changed.");
        }
    }
}

void DisplaySignees()
{
    foreach(var student in attendanceService.StudentToSign())
    {
        Console.WriteLine($"Student {student.firstName} {student.lastName}");
    }
}

void DisplayAbsencesPerStudent(DateTime startDate)
{
    var attendanceRecordService = new AttendanceRecordService();
    AttendanceRecord attendanceHistory = attendanceRecordService.LoadAttendanceHistory();

    // A dictionary to keep track of each student's absences.
    var studentAbsences = new Dictionary<int, int>(); // Student ID and count of absences

    foreach (var record in attendanceHistory.AttendanceLog)
    {
        // Filter records that are on or after the start date.
        if (record.recordDate >= startDate)
        {
            foreach (var student in record.StudentsData)
            {
                // Initialize absence count if this is the first time we've seen this student.
                if (!studentAbsences.ContainsKey(student.Id))
                {
                    studentAbsences[student.Id] = 0;
                }

                // If the student was absent, increment their absence count.
                if (student.AttendanceStatus == AttendanceStatus.Absent)
                {
                    studentAbsences[student.Id]++;
                }
            }
        }
    }

    // Display the results.
    foreach (var kvp in studentAbsences)
    {
        Console.WriteLine($"Student ID: {kvp.Key} has been absent {kvp.Value} times since {startDate:yyyy-MM-dd}.");
    }
}

Console.WriteLine(@" _    _        _                                       _____                    _   
| |  | |      | |                                     |  __ \                  | |  
| |  | |  ___ | |  ___  ___   _ __ ___    ___         | |  \/ _   _   ___  ___ | |_ 
| |/\| | / _ \| | / __|/ _ \ | '_ ` _ \  / _ \        | | __ | | | | / _ \/ __|| __|
\  /\  /|  __/| || (__| (_) || | | | | ||  __/        | |_\ \| |_| ||  __/\__ \| |_ 
 \/  \/  \___||_| \___|\___/ |_| |_| |_| \___|         \____/ \__,_| \___||___/ \__|
                                                                                    
                                                                                    ");

int attempt = 0;
const int maxAttempts = 5;

while (attempt < maxAttempts)
{
    Console.Write("Enter username: ");
    string username = Console.ReadLine();

    Console.Write("Enter password: ");
    string password = Console.ReadLine();

    if (username == "admin" && password == "admin")
    {
        Console.Clear();
        break;
    }
    else
    {
        attempt++;
        Console.WriteLine("Incorrect username or password. Please try again.");

        if (attempt < maxAttempts)
        {
            Console.WriteLine($"You have {maxAttempts - attempt} attempts left.");
        }
        else
        {
            Console.WriteLine("You have reached the maximum number of attempts. The application will now exit.");
            Environment.Exit(0);
        }
    }
}

bool exitApp = false;
Console.WriteLine(@"  ___                                        _____                      _              _ 
 / _ \                                      |  __ \                    | |            | |
/ /_\ \  ___   ___   ___  ___  ___          | |  \/ _ __   __ _  _ __  | |_   ___   __| |
|  _  | / __| / __| / _ \/ __|/ __|         | | __ | '__| / _` || '_ \ | __| / _ \ / _` |
| | | || (__ | (__ |  __/\__ \\__ \         | |_\ \| |   | (_| || | | || |_ |  __/| (_| |
\_| |_/ \___| \___| \___||___/|___/          \____/|_|    \__,_||_| |_| \__| \___| \__,_|
                                                                                         
                                                                                         ");
while (!exitApp)
{
    Console.WriteLine("\nChoose an option:");
    Console.WriteLine("\t1. Display students");
    Console.WriteLine("\t2. Mark Absence");
    Console.WriteLine("\t3. Display students who need to sign");
    Console.WriteLine("\t4. Total absences per student");
    Console.WriteLine("\t5. Modify Admin password");
    Console.WriteLine("\t6. Exit Application");
    Console.Write("\r\nSelect an option: ");

    switch (Console.ReadLine().Trim())
    {
        case "1":
            Console.Clear();
            DisplayStudents();
            break;
        case "2":
            Console.Clear();
            MarkAbsence();
            break;
        case "3":
            Console.Clear();
            DisplaySignees();
            break;
        case "4":
            Console.Clear();
            DisplayAbsencesPerStudent(new DateTime(2023, 10, 1));
            break;
        case "5":
            Console.Clear();
            authenticationService.UpdatePwd(Console.ReadLine());
            break;
        case "6":
            exitApp = true;
            break;
        default:
            Console.Clear();
            Console.WriteLine("Please enter a valid option.");
            break;
    }
}

Console.WriteLine("Exiting application...");