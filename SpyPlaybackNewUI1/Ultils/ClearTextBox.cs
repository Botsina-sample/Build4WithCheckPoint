using Gu.Wpf.UiAutomation;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace SpyandPlaybackTestTool.Ultils
{
    /**
    * ClearTextBox class use to Clear the input value in the AUT.
    */

    internal class ClearTextBox
    {
        /// <summary>
        /// WPF UI Automation Elements
        /// </summary>
        private static IReadOnlyList<UiElement> ElementListWPF;

        /// <summary>
        /// Win32 Native UI Automation Elements (MessageBox) only
        /// </summary>
        private static IReadOnlyList<UiElement> ElementListWin32;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Clear the value of textboxes or any control type with Edit
        /// </summary>
        /// <param name="Proc"></param>
        public static void ClearValue(string Proc)
        {
            var curtime = DateTime.Now;

            ExceptionCode excode = new ExceptionCode();

            try
            {
                log.Info("BEGIN CLEAR VALUE");
                Process targetProcess = WindowInteraction.GetProcess(Proc);
                GrabAUT.GetMainWindow();
                WindowInteraction.FocusWindow(targetProcess);

                ElementListWPF = GrabAUT.SearchbyFramework("WPF");

                //ElementListWin32 = DoSpy.SearchbyFramework("Win32");

                //if (ElementListWin32[0].AsMessageBox().)
                //{
                //    ElementListWin32[0].AsMessageBox().Close();
                //}

                int id = 0;

                foreach (UiElement UIE in ElementListWPF)
                {
                    if (UIE.ControlType.ProgrammaticName == "ControlType.Edit")
                    {
                        if (!UIE.AsTextBox().IsReadOnly)
                        {
                            UIE.AsTextBox().Enter("");
                            //(UIE.AutomationElement.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern).SetValue("");
                            log.Info(id + " CLEARED");
                        }
                    }
                    else if (UIE.ClassName == "CheckBox")
                    {
                        if (UIE.AsCheckBox().IsChecked == true)
                        {
                            UIE.AsCheckBox().Click();
                            log.Info(id + " UNCHECKED");
                        }
                    }

                    id++;
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == excode.INVALID_SCRIPT)
                {
                    log.Error("CANNOT USE THE CURRENT SCRIPT ON THIS SCREEN");
                }
                else
                {
                    log.Error(ex.Message);
                }
            }
        }
    }
}