using Gu.Wpf.UiAutomation;

using SpyandPlaybackTestTool.SpyPlaybackObjects;

namespace SpyandPlaybackTestTool.Actions
{
    internal abstract class AbsAction
    {
        public PlaybackObject PlaybackObject { get; set; }

        /// <summary>
        /// Properties of AUT UI Components
        /// </summary>
        public SpyObject SpyObject { get; set; }

        /// <summary>
        /// WPF UI Automation Elements
        /// </summary>
        public UiElement UiElement;

        /// <summary>
        /// Return true or false if playback is success or fail
        /// </summary>
        public bool Result;

        /// <summary>
        /// Execute action for UI Components
        /// </summary>
        public abstract void DoExecute();
    }
}