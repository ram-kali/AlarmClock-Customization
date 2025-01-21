using ConsoleApp1.keyword;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;

namespace ConsoleApp1.Pages
{
    
    public class Task2_Page : GenericKeyword
    {
       
        private const string HourPickerAutomationID = "HourPicker";
        private const string MinutePickerAutomationID = "MinutePicker";
        private const string RepeatCheckBoxAutomationID = "RepeatCheckBox";
        private const string AddAlarmButtonAutomationID = "AddAlarmButton";
        private const string SaveButtonName = "Save";



        /// <summary>
        /// Navigates to a specified menu tab by its button name.
        /// </summary>
        /// <param name="app">The application instance used to interact with the UI.</param>
        /// <param name="automation">The automation instance used for UI interaction with the app.</param>
        /// <param name="buttonName">The name of the button/tab to select in the menu.</param>
        public void navigateMenuTab(AutomationElement mainWindow, string buttonName)
        {
            try
            {     
                ClickOn(mainWindow, "Name", buttonName);
                stepPass($"Select '{buttonName}' tab in Menu");
            }
            catch (Exception e)
            {
                stepFail($"Failed to select '{buttonName}' tab in Menu. Error: {e.Message}, StackTrace: {e.StackTrace}");
            }
        }

        /// <summary>
        /// Clicks the "Add Alarm" button to initiate the creation of a new alarm.
        /// </summary>
        /// <param name="app">The application instance used to interact with the UI.</param>
        /// <param name="automation">The automation instance used for UI interaction with the app.</param>
        public void clickAddAlarm(AutomationElement mainWindow)
        {
            try
            {
                wait(1);
                ClickOn(mainWindow, "AutomationID", AddAlarmButtonAutomationID);
                stepPass("Click 'AddAlarmButton' Button");
                wait(3);
            }
            catch (Exception e)
            {
                stepFail($"Failed to create New Alarm. Error: {e.Message}, StackTrace: {e.StackTrace}");
            }
        }

        /// <summary>
        /// Sets the alarm time by adjusting hour and minute pickers in the UI.
        /// </summary>
        /// <param name="app">The application instance used to interact with the UI.</param>
        /// <param name="automation">The automation instance used for UI interaction with the app.</param>
        /// <param name="hour">The hour to set the alarm to.</param>
        /// <param name="minutes">The minutes to set the alarm to.</param>
        public void setAlarmTime(AutomationElement mainWindow, int hour, int minutes)
        {
            try
            {

                // Adjust the hour picker based on the provided hour
                if (hour > 7)
                {
                    ClickOn(mainWindow, "AutomationID", "HourPicker");
                    wait(1);
                    for (int times = 0; times < (hour - 7); times++)
                    {
                        Keyboard.Press(VirtualKeyShort.UP);
                        wait(1);
                        
                    }
                    stepPass($"The hour is set to {hour}");
                }
                else
                {
                    ClickOn(mainWindow, "AutomationID", "HourPicker");
                    for (int times = 0; times < (7 - hour); times++)
                    {
                        Keyboard.Press(VirtualKeyShort.DOWN);
                    }
                    stepPass($"The hour timer is set to {hour} hour");
                }

                // Adjust the minute picker based on the provided minutes
                if (minutes > 0)
                {
                    ClickOn(mainWindow, "AutomationID", "MinutePicker");
                    for (int times = 0; times < minutes; times++)
                    {
                        Keyboard.Press(VirtualKeyShort.UP);
                    }
                    stepPass($"The alarm time is set to {minutes} minutes");
                }
                else
                {
                    stepPass("The minutes timer is set to 00 minutes");
                }
            }
            catch (Exception e)
            {
                stepPass($"Failed to set Alarm Time. Error: {e}");
            }
        }

        /// <summary>
        /// Sets the name for the alarm in the application.
        /// </summary>
        /// <param name="app">The application instance used to interact with the UI.</param>
        /// <param name="automation">The automation instance used for UI interaction with the app.</param>
        /// <param name="alarmName">The name to set for the alarm.</param>
        public void setAlarmName(AutomationElement mainWindow, string alarmName)
        {
            try
            {
                
                EnterText(mainWindow, "Name", "Alarm name", alarmName);
                stepPass($"The Alarm Name is set to '{alarmName}'");
            }
            catch (Exception e)
            {
                stepFail($"Failed to set Alarm Name. Error: {e.Message}, StackTrace: {e.StackTrace}");
            }
        }

        /// <summary>
        /// Sets the alarm to repeat based on the provided condition.
        /// </summary>
        /// <param name="app">The application instance used to interact with the UI.</param>
        /// <param name="automation">The automation instance used for UI interaction with the app.</param>
        /// <param name="condition">A boolean that determines whether the alarm should repeat. True enables repeating, false disables it.</param>
        public void setRepeatedAlarm(AutomationElement mainWindow, bool condition)
        {
            try
            {
                if (condition)
                {
                    ClickCheckBox(mainWindow, "AutomationID", RepeatCheckBoxAutomationID);
                    stepPass("Repeat Alarm Set Successfully!");
                }
                else
                {
                    stepPass("Repeat Alarm CheckBox is not turned on");
                }
            }
            catch (Exception e)
            {
                stepFail($"Failed to Turn Off/On Repeated Alarm. Error: {e.Message}, StackTrace: {e.StackTrace}");
            }
        }

        /// <summary>
        /// Selects specific days for the alarm to repeat.
        /// </summary>
        /// <param name="app">The application instance used to interact with the UI.</param>
        /// <param name="automation">The automation instance used for UI interaction with the app.</param>
        /// <param name="days">A list of day names to select for the alarm repetition.</param>
        public void selectAlarmDays(AutomationElement mainWindow, List<string> days)
        {
            try
            {
                for (int i = 0; i < days.Count; i++)
                {
                    ClickOn(mainWindow, "Name", days[i]);
                    stepPass($"Select {days[i]}");
                }
            }
            catch (Exception e)
            {
                stepFail($"Failed to select Alarm Days. Error: {e.Message}, StackTrace: {e.StackTrace}");
            }
        }

        /// <summary>
        /// Saves the alarm after it has been configured.
        /// </summary>
        /// <param name="app">The application instance used to interact with the UI.</param>
        /// <param name="automation">The automation instance used for UI interaction with the app.</param>
        public void saveAlarm(AutomationElement mainWindow)
        {
            try
            {
                ClickOn(mainWindow, "Name", SaveButtonName);
                stepPass("Alarm saved successfully");
            }
            catch (Exception e)
            {
                stepFail($"Failed to save Alarm. Error: {e.Message}, StackTrace: {e.StackTrace}");
            }
        }

        /// <summary>
        /// Deletes an alarm based on the given alarm name.
        /// </summary>
        /// <param name="app">The application instance used to interact with the UI.</param>
        /// <param name="automation">The automation instance used for UI interaction with the app.</param>
        /// <param name="AlarmName">The name of the alarm to delete.</param>
        public void deleteAlarm(AutomationElement mainWindow, string AlarmName)
        {
            try
            {
                wait(2);
                ClickOn(mainWindow, "Name", AlarmName);
                wait(1); // Optional wait before clicking the delete button
                ClickOn(mainWindow, "AutomationID", "DeleteButton");
                stepPass("Alarm deleted successfully");
            }
            catch (Exception e)
            {
                stepFail($"Failed to Delete Alarm. Error: {e.Message}, StackTrace: {e.StackTrace}");
            }
        }
    }
}
