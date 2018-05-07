using SpyandPlaybackTestTool.CheckPoints;
namespace SpyandPlaybackTestTool.SpyPlaybackObjects
{
    public class PlaybackObject
    {
        public int index { get; set; }
        public string automationId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string action { get; set; }
        public string text { get; set; }
        public int itemIndex { get; set; }
        public AbsCheckPoint CheckPoint;
    }
}