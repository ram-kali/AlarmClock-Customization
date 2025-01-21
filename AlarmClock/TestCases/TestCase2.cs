using ConsoleApp1.keyword;
using ConsoleApp1.Pages;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using Application = FlaUI.Core.Application;

namespace ConsoleApp1.TestCases
{
    class TestCase2:Task2_Page
    {
        static GenericKeyword keyword = new GenericKeyword();
        static UIA3Automation automation = new UIA3Automation();
        static Application app = keyword.LaunchStorApplication("Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");
        string AlarmName = "Trumpf Metamation - Login Time";
        List<string> days = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        static AutomationElement mainWindow = app.GetMainWindow(automation);

        public void task2()
        {
            wait(5);//waiting for application to load
            navigateMenuTab(mainWindow, "Alarm");
            clickAddAlarm(mainWindow);
            setAlarmTime(mainWindow, 9, 0);
            setAlarmName(mainWindow, AlarmName);
            setRepeatedAlarm(mainWindow, true);
            selectAlarmDays(mainWindow, days);
            saveAlarm(mainWindow);
            deleteAlarm(mainWindow, AlarmName);
            app.Close();

        }
        

    }
}