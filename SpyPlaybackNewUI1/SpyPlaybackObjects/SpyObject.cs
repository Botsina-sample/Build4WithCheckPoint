using System.Collections.Generic;

namespace SpyandPlaybackTestTool.SpyPlaybackObjects
{
    public class SpyObject
    {
        public int index { get; set; }
        public string type { get; set; }
        public string automationId { get; set; }
        public string name { get; set; }
        public List<string> itemList = new List<string>();
    }
}