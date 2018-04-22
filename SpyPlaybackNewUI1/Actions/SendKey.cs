using System;
using System.Windows.Forms;

namespace SpyandPlaybackTestTool.Actions
{
    internal class SendKey : AbsAction
    {
        //public override void ExecuteCheckPoint()
        //{
        //    throw new NotImplementedException();
        //}
        public override void DoExecute()
        {
            try
            {
                SendKeys.SendWait("{" + PlaybackObject.text + "}");
                Result = true;
            }
            catch (Exception)
            {
                //throw;
                Result = false;
            }
        }
    }
}