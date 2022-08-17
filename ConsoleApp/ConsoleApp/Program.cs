using System;
using System.Runtime.InteropServices;
#nullable enable

namespace ConsoleAppWithNewCompiler
{

    public struct Student
    {
        public string FirstName;
        public string? MiddleName;
        public string LastName;
    }

    public static class Program
    {
        public static void PrintStudent(Student student)
        {
			
			Console.WriteLine(RuntimeInformation.FrameworkDescription);

			Console.Write("\npress any key to continue...");
			Console.ReadKey();
            // simple nullable check in compiler analysis
            string name = null;
            var myName = name.ToString();

            Console.WriteLine($"First name: {student.FirstName.ToUpper()}");
            Console.WriteLine($"Middle name: {student.MiddleName?.ToUpper()}");
            Console.WriteLine($"Last name: {student.LastName.ToUpper()}");
        }

        public static void Main() => PrintStudent(default);
    }
}
