using ConsoleApp1.Utilities;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using System.Diagnostics;


namespace ConsoleApp1.keyword
{
    public class GenericKeyword : BaseClass
    {
        ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

        /// <summary>
        /// Creates a new directory at the specified path if it does not exist.
        /// </summary>
        /// <param name="folderPath">The path of the directory to create.</param>
        public void CreateDirectory(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                stepPass($"New Directory created Successfully. Path: {folderPath}");
            }
            else
            {
                stepFail("Failed to create Directory!");
            }
        }

        /// <summary>
        /// Creates a new text file with specified content at the given path.
        /// </summary>
        /// <param name="textFilePath">The path where the text file will be created.</param>
        /// <param name="textToBeWritten">The content to write into the text file.</param>
        public void CreateNewtextFileWithText(string textFilePath, string textToBeWritten)
        {
            if (!File.Exists(textFilePath))
            {
                File.WriteAllText(textFilePath, textToBeWritten);
                stepPass("New text File Created. Path '" + textFilePath + "'");
                stepPass("Write text in text file was successful!");
            }
            else
            {
                stepFail("Failed to write text in Text File");
            }
        }

        /// <summary>
        /// Starts an application with the given name and folder path.
        /// </summary>
        /// <param name="appName">The name of the application to start.</param>
        /// <param name="folderPath">The folder path where the application is located (optional).</param>
        /// <returns>The Process instance of the started application.</returns>
        public Process StartApplication(string appName, string folderPath)
        {
            return folderPath != null ? Process.Start(appName, folderPath) : Process.Start(appName);
        }

        /// <summary>
        /// Deletes the folder at the specified path, along with its contents.
        /// </summary>
        /// <param name="folderPath">The path of the folder to delete.</param>
        public void DeleteFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
                stepPass("Folder and its contents deleted successfully!");
                return;
            }
            stepFail("Failed to delete folder and its contents!");
        }

        /// <summary>
        /// Launches an application specified by the given application path.
        /// </summary>
        /// <param name="application">The path to the application to launch.</param>
        /// <returns>The Application instance of the launched application.</returns>
        public Application LaunchApplication(string application)
        {
            return Application.Launch(application);
        }

        /// <summary>
        /// Launches a store application specified by the given application identifier.
        /// </summary>
        /// <param name="application">The application identifier to launch.</param>
        /// <returns>The Application instance of the launched store app.</returns>
        public Application LaunchStorApplication(string application)
        {
            return Application.LaunchStoreApp(application);
        }

        /// <summary>
        /// Retrieves the main window of the specified application.
        /// </summary>
        /// <param name="app">The application from which to retrieve the main window.</param>
        /// <param name="automation">The automation instance used for interaction with the UI.</param>
        /// <returns>The main window of the application.</returns>
        public Window GetCurrentWindow(Application app, UIA3Automation automation)
        {
            return app.GetMainWindow(automation);
        }

        /// <summary>
        /// Clicks on a button element in the main window of the application.
        /// </summary>
        /// <param name="mainWindow">The main window of the application.</param>
        /// <param name="elementType">The type of the element (e.g., "name", "automationid", "classname").</param>
        /// <param name="element">The identifier for the element to click.</param>
        public void ClickOn(Window mainWindow, string elementType, string element)
        {
            findElement(mainWindow, elementType, element).AsButton().Click();
        }

        /// <summary>
        /// Clicks on a checkbox element in the main window of the application.
        /// </summary>
        /// <param name="mainWindow">The main window of the application.</param>
        /// <param name="elementType">The type of the element (e.g., "name", "automationid", "classname").</param>
        /// <param name="element">The identifier for the checkbox to click.</param>
        public void ClickCheckBox(Window mainWindow, string elementType, string element)
        {
            findElement(mainWindow, elementType, element).AsCheckBox().Click();
        }

        /// <summary>
        /// Enters text into a text box element in the main window of the application.
        /// </summary>
        /// <param name="mainWindow">The main window of the application.</param>
        /// <param name="elementType">The type of the element (e.g., "name", "automationid", "classname").</param>
        /// <param name="element">The identifier for the text box element.</param>
        /// <param name="text">The text to enter into the text box.</param>
        public void EnterText(Window mainWindow, string elementType, string element, string text)
        {
            findElement(mainWindow, elementType, element).AsTextBox().Enter(text);
        }

        /// <summary>
        /// Finds an element in the main window by its type and identifier.
        /// </summary>
        /// <param name="mainWindow">The main window of the application.</param>
        /// <param name="elementType">The type of the element (e.g., "name", "automationid", "classname").</param>
        /// <param name="element">The identifier for the element to find.</param>
        /// <returns>The UI element found in the window, or null if not found.</returns>
        public Window findElement(Window mainWindow, string elementType, string element)
        {
            switch (elementType.ToLower())
            {
                case "name":
                    return (Window)mainWindow.FindFirstDescendant(cf.ByName(element));
                case "automationid":
                    return (Window)mainWindow.FindFirstDescendant(cf.ByAutomationId(element));
                case "classname":
                    return (Window)mainWindow.FindFirstDescendant(cf.ByClassName(element));
                default:
                    return null;
            }
        }
    }
}
