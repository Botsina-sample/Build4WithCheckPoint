using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyandPlaybackTestTool.Actions
{
    internal class PasswordBoxAction : AbsAction
    {
        //public override void ExecuteCheckPoint()
        //{
        //    throw new NotImplementedException();
        //}
        public override void DoExecute()
        {
            switch(PlaybackObject.action)
            {
                case "SetText":
                    try
                    {

                        UiElement.AsTextBox().Enter(PlaybackObject.text);
                        Result = false;

                    } catch (Exception)
                    {
                        Result = true;
                    }

                    break;
                default:
           
                    break;
                        
            }
        }
    }
}
