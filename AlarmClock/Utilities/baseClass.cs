using System;
using System.IO;
using System.Threading;

namespace ConsoleApp1.Utilities
{
    public class BaseClass
    {
        static string currentDirectory = System.IO.Directory.GetCurrentDirectory();
        private static readonly string LogFilePath = currentDirectory + "../../../../TestResults/log.txt";
        private static StreamWriter _logFile;

        public static void CreateLogFile()
        {
            // Ensure the old log file is deleted
            if (File.Exists(LogFilePath))
            {
                File.Delete(LogFilePath);
                Console.WriteLine("Log file deleted successfully.");
            }
            else
            {
                Console.WriteLine("Log file not found. Creating a new one...");
            }

            // Initialize the StreamWriter and redirect Console output
            _logFile = new StreamWriter(LogFilePath, append: true) { AutoFlush = true };
            Console.SetOut(_logFile);
        }

        public static void CloseLogFile()
        {
            // Ensure StreamWriter is closed properly
            if (_logFile != null)
            {
                _logFile.Close();
                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
                Console.WriteLine("Log file closed.");
            }
        }

        public static void stepPass(string statement)
        {
            Console.WriteLine($"[PASS]: {statement}");
        }

        public static void stepFail(string statement)
        {
            Console.WriteLine($"[FAIL]: {statement}");
        }

        public static void stepInfo(string statement)
        {
            Console.WriteLine($"[INFO]: {statement}");
        }

        public void wait(int seconds)
        {
            Console.WriteLine($"[INFO]: Waiting for {seconds} seconds...");
            Thread.Sleep(seconds * 1000);
        }

        public static void beforeAll()
        {
            Console.WriteLine("[INFO]: Running setup (BeforeAll)...");
            CreateLogFile();
            stepInfo("Setup completed.");
        }

        public static void afterAll()
        {
            Console.WriteLine("[INFO]: Running cleanup (AfterAll)...");
            CloseLogFile();
            stepInfo("Cleanup completed.");
        }
    }
}
