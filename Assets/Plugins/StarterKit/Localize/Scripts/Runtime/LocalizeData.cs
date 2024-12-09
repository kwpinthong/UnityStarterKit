using System.Collections.Generic;

namespace StarterKit.LocalizeLib.Runtime
{
    [System.Serializable]
    public class LocalizeData
    {
        [System.Serializable]
        public class Info
        {
            public string Code;
            public string Text;
        }

        public string Key;
        public List<Info> LocalizeInfos = new List<Info>();
    }
}
