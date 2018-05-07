#region old
//using System;
//using System.Windows.Automation;

//namespace SpyandPlaybackTestTool.Actions
//{
//    internal class ComboBoxAction : AbsAction
//    {
//        public override void DoExecute()
//        {
//            AutomationElement a = UiElement.AutomationElement;

//            ExpandCollapsePattern expandCollapsePattern = a.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
//            expandCollapsePattern.Expand();
//            var components = a.FindAll(TreeScope.Subtree, Condition.TrueCondition);

//            var comboBoxEditItemCondition = new PropertyCondition(AutomationElement.ClassNameProperty, "ListBoxItem");
//            var listItems = a.FindAll(TreeScope.Subtree, comboBoxEditItemCondition);//It can only get one item in the list (the first one).

//            foreach (AutomationElement a5 in listItems)
//            {
//                (a5.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern).Select();
//            }

//            switch (PlaybackObject.action)
//            {
//                case "SetText":
//                    try
//                    {
//                        ((ValuePattern)a.GetCurrentPattern(ValuePattern.Pattern)).SetValue(PlaybackObject.text);

//                        Result = true;
//                    }
//                    catch (Exception)
//                    {
//                        Result = false;
//                        throw;
//                    }
//                    break;

//                case "Select":
//                    try
//                    {
//                        (listItems[PlaybackObject.itemIndex].GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern).Select();
//                        Result = true;
//                    }
//                    catch (Exception)
//                    {
//                        Result = false;
//                        throw;
//                    }
//                    break;

//                default:
//                    Result = false;
//                    break;
//            }

//            expandCollapsePattern.Collapse();
//        }
//    }
//}
#endregion

<<<<<<< HEAD
<<<<<<< HEAD
using Gu.Wpf.UiAutomation;
=======
>>>>>>> 2ce5e3a5c1465a69dec9e480c64d73a27be85ae8
=======
>>>>>>> 2ce5e3a5c1465a69dec9e480c64d73a27be85ae8
using System;
using System.Windows.Automation;

namespace SpyandPlaybackTestTool.Actions
{
    internal class ComboBoxAction : AbsAction
    {
        public override void DoExecute()
        {


            switch (PlaybackObject.action)
            {
                case "SetText":
                    try
                    {
                        if (UiElement.AsComboBox().IsEditable.Equals(true))
                        {
                            UiElement.AsTextBox().Enter(PlaybackObject.text);
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

                case "Select":
                    try
                    {
<<<<<<< HEAD
<<<<<<< HEAD
                        //AutomationElement a = UiElement.AutomationElement;
                        //ExpandCollapsePattern expandCollapsePattern = a.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
                        //expandCollapsePattern.Expand();
                        //var cbxEditItems = UiElement.FindAll(TreeScope.Subtree, new System.Windows.Automation.PropertyCondition(AutomationElement.ClassNameProperty, "ComboBoxItem"));
                        //cbxEditItems[PlaybackObject.itemIndex].AutomationElement.SelectionItemPattern().Select();
                        //expandCollapsePattern.Collapse();
                        UiElement.AsComboBox().Select(PlaybackObject.itemIndex);
                        UiElement.AsComboBox().Collapse();
=======
                        UiElement.AsComboBox().SelectedIndex = PlaybackObject.itemIndex;
                        //UiElement.AsComboBox().Collapse();
>>>>>>> 2ce5e3a5c1465a69dec9e480c64d73a27be85ae8
=======
                        UiElement.AsComboBox().SelectedIndex = PlaybackObject.itemIndex;
                        //UiElement.AsComboBox().Collapse();
>>>>>>> 2ce5e3a5c1465a69dec9e480c64d73a27be85ae8
                        Result = true;
                    }
                    catch (Exception)
                    {
                        Result = false;

                    }
                    break;

                default:
                    Result = false;
                    break;
            }
        }
    }
}