using System;
using System.Runtime.InteropServices;

namespace SpyandPlaybackTestTool.Actions
{
    internal class TextBoxAction : AbsAction
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        //public override void ExecuteCheckPoint()
        //{
        //    CpResult = new bool[4];
        //    for (int i = 0; i < 4; i++)
        //        CpResult[i] = false;
        //    if (cpIsEmpty == true)
        //    {
        //        try
        //        {
        //            object a = UiElement.AsTextBox().Text;
        //            object b = "";

        //            if (a.Equals(b))
        //                CpResult[0] = true;
        //            else
        //                CpResult[0] = false;
        //        }
        //        catch (Exception)
        //        {
        //            CpResult[0] = false;
        //        }
        //    }
        //    if (cpIsReadOnly == true)
        //    {
        //        try
        //        {
        //            if (UiElement.AsTextBox().IsReadOnly)
        //                CpResult[1] = true;
        //            else
        //                CpResult[1] = false;
        //        }
        //        catch (Exception)
        //        {
        //            CpResult[1] = false;
        //        }
        //    }

        //    if (cpIsEqual==true)
        //    {
        //        try
        //        {

        //            string a = UiElement.AsTextBox().Text;
        //            string b = this.expectedVal;

        //            if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
        //            {
        //                a = "0";
        //                b = a;
        //            }

        //            if (a.Equals(b))
        //                CpResult[2] = true;
        //            else
        //                CpResult[2] = false;
        //        }
        //        catch (Exception)
        //        {
        //            CpResult[2] = false;
        //        }
        //    }
        //    if (cpIsEnabled == true)
        //    {
        //        try
        //        {
        //            if (UiElement.AsTextBox().IsEnabled)
        //                CpResult[3] = true;
        //            else
        //                CpResult[3] = false;
        //        }
        //        catch (Exception)
        //        {
        //            CpResult[3] = false;
        //        }
        //    }
        //}
        public override void DoExecute()
        {
            switch (PlaybackObject.action)
            {
                case "SetText":
                    try
                    {
                        #region vinh's date validation
                        //if (UiElement.AsTextBox().Text == "__/__/____")
                        //{
                        //    if (PlaybackObject.text.Count() == 8)
                        //    {
                        //        string date = PlaybackObject.text.Trim();
                        //        date = date.Substring(0, 2) + "/" + date.Substring(2, 2) + "/" + date.Substring(4, 4);
                        //        try
                        //        {
                        //            DateTime dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //            //(UiElement.AutomationElement.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern).SetValue(PlaybackObject.text);
                        //            UiElement.AsTextBox().Enter(PlaybackObject.text);
                        //        }
                        //        catch
                        //        {
                        //            MessageBox.Show("Date input is invalid");
                        //            Result = false;
                        //        }
                        //        if (UiElement.AsTextBox().Text == date)
                        //            Result = true;
                        //        else
                        //            Result = false;
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine("You have to enter exactly dd/MM/yyyy");
                        //        Result = false;
                        //    }
                        //}
                        //else
                        //{

                        #endregion

                        bool CapsLockOn = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
                        if (CapsLockOn == true)
                        {
                            //Console.WriteLine(CapsLock);
                            const int KEYEVENTF_EXTENDEDKEY = 0x1;
                            const int KEYEVENTF_KEYUP = 0x2;
                            keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                            keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
                            (UIntPtr)0);
                        }
                        //(UiElement.AutomationElement.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern).SetValue(PlaybackObject.text);
                        UiElement.AsTextBox().Enter(PlaybackObject.text);
                        int n;
                        bool isNumeric = int.TryParse(PlaybackObject.text, out n);

                        if (UiElement.AsTextBox().Text == "__/__/____" && isNumeric == false)
                        {
                            Result = false;
                        }

                        else
                            Result = true;
                    }
                    //}
                    catch (Exception)
                    {
                        Result = false;
                    }
                    break;
                #region actions
                //case "IsEmpty":

                //    try
                //    {
                //        object a = UiElement.AsTextBox().Text;
                //        object b = "";

                //        if (a.Equals(b))
                //            Result = true;
                //        else
                //            Result = false;
                //    }
                //    catch (Exception)
                //    {
                //        Result = false;
                //    }

                //    break;

                //case "IsEqual":

                //    try
                //    {

                //        string a = UiElement.AsTextBox().Text;
                //        string b = PlaybackObject.text;

                //        if(string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
                //        {
                //            a = "0";
                //            b = a;
                //        }

                //        if (a.Equals(b))
                //            Result = true;
                //        else
                //            Result = false;
                //    }
                //    catch (Exception)
                //    {
                //        Result = false;
                //    }

                //    break;

                //case "IsReadOnly":

                //    try
                //    {
                //        if (UiElement.AsTextBox().IsReadOnly)
                //            Result = true;
                //        else
                //            Result = false;
                //    }
                //    catch (Exception)
                //    {
                //        Result = false;
                //    }

                //    break;

                //case "IsEnabled":

                //    try
                //    {
                //        if (UiElement.AsTextBox().IsEnabled)
                //            Result = true;
                //        else
                //            Result = false;
                //    }
                //    catch (Exception)
                //    {
                //        Result = false;
                //    }

                //    break;
                #endregion 
                default:
                    //Result = false;
                    //Thread.Sleep(2000);
                    break;
            }
        }
    }
}