using log4net;
using System;
using System.Reflection;




namespace SpyandPlaybackTestTool.Actions
{
    internal class RadioButtonAction : AbsAction
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void DoExecute()
        {
            switch (PlaybackObject.action)
            {
                case "Click":
                    try
                    {
                        UiElement.AsRadioButton().Focus();

                        UiElement.AsRadioButton().Click();
                        Result = true;
                    }
                    catch (Exception ex)
                    {
                        Result = false;
                        log.Debug(ex.Message);
                    }
                    break;

                case "IsChecked":
                    try
                    {
                        if (UiElement.AsRadioButton().IsChecked == true)
                        {
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
                    }
                    break;

                case "IsUnChecked":
                    try
                    {
                        if (UiElement.AsRadioButton().IsChecked == false)
                        {
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