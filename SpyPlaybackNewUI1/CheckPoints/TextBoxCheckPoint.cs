using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyandPlaybackTestTool.CheckPoints
{
    public class TextBoxCheckPoint : AbsCheckPoint
    {
        public bool cpIsEmpty { get; set; }
        public bool cpIsEmptyResult { get; set; }
        public bool cpIsReadOnly { get; set; }
        public bool cpIsReadOnlyResult { get; set; }
        public bool cpIsEnabled { get; set; }
        public bool cpIsEnabledResult { get; set; }
        public bool cpIsEqual { get; set; }
        public bool cpIsEqualResult { get; set; }
        public string expectedVal { get; set; }
        public string[] cpList = { "IsEmpty", "IsReadOnly", "IsEnabled", "IsEqual" };
        public override void DoExecuteCheckPoint()
        {
            if (cpIsEmpty == true)
            {
                try
                {
                    object a = UiElement.AsTextBox().Text;
                    object b = "";

                    if (a.Equals(b))
                        cpIsEmpty = true;
                    else
                        cpIsEmpty = false;
                }
                catch (Exception)
                {
                    cpIsEmpty = false;
                }
            }
            if (cpIsReadOnly == true)
            {
                try
                {
                    if (UiElement.AsTextBox().IsReadOnly)
                        cpIsReadOnlyResult = true;
                    else
                        cpIsReadOnlyResult = false;
                }
                catch (Exception)
                {
                    cpIsReadOnlyResult = false;
                }
            }

            if (cpIsEqual == true)
            {
                try
                {

                    string a = UiElement.AsTextBox().Text;
                    string b = this.expectedVal;

                    if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
                    {
                        a = "0";
                        b = a;
                    }

                    if (a.Equals(b))
                        cpIsEqualResult = true;
                    else
                        cpIsEqualResult = false;
                }
                catch (Exception)
                {
                    cpIsEqualResult = false;
                }
            }
            if (cpIsEnabled == true)
            {
                try
                {
                    if (UiElement.AsTextBox().IsEnabled)
                        cpIsEnabledResult = true;
                    else
                        cpIsEnabledResult = false;
                }
                catch (Exception)
                {
                    cpIsEnabledResult = false;
                }
            }
        }
    }
}
