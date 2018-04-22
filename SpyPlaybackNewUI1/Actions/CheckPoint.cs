using Gu.Wpf.UiAutomation;
using log4net;
using SpyandPlaybackTestTool.SpyPlaybackObjects;
using SpyandPlaybackTestTool.Ultils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpyandPlaybackTestTool.Actions
{
    internal class CheckPoint : AbsAction
    {
        public string ProcessName { get; set; }//nhớ sửa lại bỏ vào AUT path

        private IReadOnlyList<UiElement> ElementList;


        private SpyObject[] SpyObjectList;

        private Users theUser = new Users();
        private Process thisProc = Process.GetCurrentProcess();

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //public override void ExecuteCheckPoint()
        //{
            
        //}
        public override void DoExecute()
        {
            switch (PlaybackObject.action)
            {
                case "IsExist":

                    try
                    {
                        //log.Info("BEGIN FIND WINDOW");
                        Process targetProcess = WindowInteraction.GetProcess(ProcessName);
                        DoSpy.GetMainWindow();
                        ElementList = DoSpy.SearchbyFramework("WPF");
                        
                        foreach(UiElement window in ElementList)
                        {
                            if(window.Window.Title == PlaybackObject.text)
                            {
                                Result = true;
                                IsExist = true;
                                return;
                            } else
                            {
                                Result = false;
                                IsNotExist = true;
                                return;
                            }
                        }
                        
                        //log.Info("DONE HIGHLIGHT");
                    }
                    catch (Exception ex)
                    {
                        Result = false;
                        log.Error("ERROR CODE: " + ex.HResult + "  -----  " + "detail: " + ex.Message);
                    }
                    break;

                case "IsNotExist":

                    try
                    {
                        //log.Info("BEGIN FIND WINDOW");
                        Process targetProcess = WindowInteraction.GetProcess(ProcessName);
                        DoSpy.GetMainWindow();
                        ElementList = DoSpy.SearchbyFramework("WPF");

                        foreach (UiElement window in ElementList)
                        {
                            if (window.Window.Title == PlaybackObject.text)
                            {

                                Result = false;
                                IsExist = false;
                                return;
                            }
                            else
                            {
                                Result = true;
                                IsNotExist = false;
                                return;
                            }
                        }

                        //log.Info("DONE HIGHLIGHT");
                    }
                    catch (Exception ex)
                    {
                        Result = false;
                        log.Error("ERROR CODE: " + ex.HResult + "  -----  " + "detail: " + ex.Message);
                    }

                    break;
                default:
                    break;
            }
        }
    }
}
