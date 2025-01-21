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
        /// Helper method to get the main window of the application.
        /// </summary>
        private Window GetMainWindow(FlaUI.Core.Application app, UIA3Automation automation)
        {
            return GetCurrentWindow(app, automation);
        }

        /// <summary>
        /// Navigates to a specified menu tab by its button name.
        /// </summary>
        /// <param name="app">The application instance used to interact with the UI.</param>
        /// <param name="automation">The automation instance used for UI interaction with the app.</param>
        /// <param name="buttonName">The name of the button/tab to select in the menu.</param>
        public void navigateMenuTab(FlaUI.Core.Application app, UIA3Automation automation, string buttonName)
        {
            try
            {
                Window mainWindow = GetCurrentWindow(app, automation);
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
        public void clickAddAlarm(FlaUI.Core.Application app, UIA3Automation automation)
        {
            try
            {
                Window mainWindow = GetMainWindow(app, automation);
                ClickOn(mainWindow, "AutomationID", AddAlarmButtonAutomationID);
                stepPass("Click 'AddAlarmButton' Button");
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
        public void setAlarmTime(FlaUI.Core.Application app, UIA3Automation automation, int hour, int minutes)
        {
            try
            {
                Window mainWindow = GetMainWindow(app, automation);

                // Use a helper method to adjust the time picker for hours and minutes
                AdjustTimePicker(mainWindow, HourPickerAutomationID, hour);
                AdjustTimePicker(mainWindow, MinutePickerAutomationID, minutes);

            }
            catch (Exception e)
            {
                stepFail($"Failed to set Alarm Time. Error: {e.Message}, StackTrace: {e.StackTrace}");
            }
        }

        /// <summary>
        /// Helper method to adjust the time picker for either hours or minutes.
        /// </summary>
        private void AdjustTimePicker(Window mainWindow, string pickerAutomationID, int timeValue)
        {
            try
            {
                if (timeValue > 7)
                {
                    ClickOn(mainWindow, "AutomationID", pickerAutomationID);
                    for (int times = 0; times < (timeValue - 7); times++)
                    {
                        Keyboard.Press(VirtualKeyShort.UP);
                    }
                    stepPass($"The time picker is set to {timeValue}");
                }
                else if (timeValue < 7)
                {
                    ClickOn(mainWindow, "AutomationID", pickerAutomationID);
                    for (int times = 0; times < (7 - timeValue); times++)
                    {
                        Keyboard.Press(VirtualKeyShort.DOWN);
                    }
                    stepPass($"The time picker is set to {timeValue}");
                }
                else
                {
                    stepPass($"The time picker is set to 7 (default)");
                }
            }
            catch (Exception e)
            {
                stepFail($"Failed to adjust the time picker. Error: {e.Message}, StackTrace: {e.StackTrace}");
            }
        }

        /// <summary>
        /// Sets the name for the alarm in the application.
        /// </summary>
        /// <param name="app">The application instance used to interact with the UI.</param>
        /// <param name="automation">The automation instance used for UI interaction with the app.</param>
        /// <param name="alarmName">The name to set for the alarm.</param>
        public void setAlarmName(FlaUI.Core.Application app, UIA3Automation automation, string alarmName)
        {
            try
            {
                Window mainWindow = GetMainWindow(app, automation);
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
        public void setRepeatedAlarm(FlaUI.Core.Application app, UIA3Automation automation, bool condition)
        {
            try
            {
                if (condition)
                {
                    Window mainWindow = GetMainWindow(app, automation);
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
        public void selectAlarmDays(FlaUI.Core.Application app, UIA3Automation automation, List<string> days)
        {
            try
            {
                Window mainWindow = GetMainWindow(app, automation);
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
        public void saveAlarm(FlaUI.Core.Application app, UIA3Automation automation)
        {
            try
            {
                Window mainWindow = GetMainWindow(app, automation);
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
        public void deleteAlarm(FlaUI.Core.Application app, UIA3Automation automation, string AlarmName)
        {
            try
            {
                Window mainWindow = GetMainWindow(app, automation);
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
