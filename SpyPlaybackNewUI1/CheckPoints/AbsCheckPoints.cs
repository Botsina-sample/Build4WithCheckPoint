using Gu.Wpf.UiAutomation;
using SpyandPlaybackTestTool.SpyPlaybackObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyandPlaybackTestTool.CheckPoints
{
    public abstract class AbsCheckPoint
    {

        public UiElement UiElement;
        public abstract void DoExecuteCheckPoint();

    }
}
