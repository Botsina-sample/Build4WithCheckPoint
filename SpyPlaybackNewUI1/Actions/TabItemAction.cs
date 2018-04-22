using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyandPlaybackTestTool.Actions
{
    internal class TabItemAction : AbsAction
    {
        //public override void ExecuteCheckPoint()
        //{
        //    throw new NotImplementedException();
        //}
        public override void DoExecute()
        {
            switch (PlaybackObject.action)
            {
                case "Click":
                    try
                    {
                        if (UiElement.AutomationElement.Current.IsEnabled)
                        {
                            UiElement.AsTabItem().Click();
                     
                            Result = true;
                        }
                        else
                        {
                            Result = false;
                        }
                    }
                    catch (Exception)
                    {
                        Result = false;
                        //throw;
                    }
                    break;

                case "IsSelected":
                    try
                    {
                     
                        if(UiElement.AsTabItem().IsSelected)
                        {
                            Result = true;
                        } else
                        {
                            Result = false;
                            return;
                        }

                    } catch(Exception)
                    {
                        Result = false;
                    }
                    break;

                default:
                    Result = false;
                    //Thread.Sleep(2000);
                    break;
            }
        }
    }
}
