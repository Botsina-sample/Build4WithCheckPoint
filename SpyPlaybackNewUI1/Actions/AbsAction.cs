using Gu.Wpf.UiAutomation;

using SpyandPlaybackTestTool.SpyPlaybackObjects;

namespace SpyandPlaybackTestTool.Actions
{
    internal abstract class AbsAction
    {
        public PlaybackObject PlaybackObject { get; set; }


        public SpyObject SpyObject { get; set; }
        public UiElement UiElement;
        public bool Result;
        public bool []CpResult;
        public bool IsExist;
        public bool IsNotExist;
        //public bool cpIsEmpty { get; set; }
        //public bool cpIsReadOnly { get; set; }
        //public bool cpIsEnabled { get; set; }
        //public bool cpIsEqual { get; set; }
        //public string expectedVal { get; set; }
        public abstract void DoExecute();
        //public abstract void ExecuteCheckPoint();
    }
}