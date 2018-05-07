using Gu.Wpf.UiAutomation;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Automation;

namespace SpyandPlaybackTestTool.Ultils
{
    internal class GrabAUT
    {
        /// <summary>
        /// Wrapper for the AUT
        /// </summary>
        public static Gu.Wpf.UiAutomation.Application App { get; set; }

        /// <summary>
        /// MainWindow of the AUT
        /// </summary>
        public static Gu.Wpf.UiAutomation.Window MainWindow { get; set; }

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region UiElement Functions

        /// <summary>
        /// Search UI by class
        /// </summary>
        /// <param name="type">Button, MessageBox, DataGrid, ComboBox...etc</param>
        /// <returns></returns>
        public static IReadOnlyList<UiElement> ElementClass(string type)
        {
            return MainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ClassNameProperty, type));
        }

        /// <summary>
        /// Search all UI by framework
        /// </summary>
        /// <param name="type">WPF or Win32</param>
        /// <returns></returns>
        public static IReadOnlyList<UiElement> SearchbyFramework(string type)
        {
            return MainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.FrameworkIdProperty, type));
        }

        public static void GetMainWindow()
        {
            try
            {
                Process AttachProcess = WindowInteraction.GetProcess(ProcessForm.targetproc);

                Application.WaitForMainWindow(AttachProcess, TimeSpan.FromSeconds(1));

                App = Application.Attach(AttachProcess.Id);

                MainWindow = App.MainWindow;
            }
            catch
            {
       
                throw new Exception("This process cannot be attached");
            }
        }

        #endregion UiElement Functions
    }
}