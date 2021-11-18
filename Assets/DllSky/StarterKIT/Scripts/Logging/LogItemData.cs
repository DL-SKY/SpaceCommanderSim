#if LOGGING

using System;
using UnityEngine;

namespace DllSky.StarterKITv2.Logging
{
    [System.Serializable]
    public class LogItemData
    {
        public LogType type;
        [TextArea] public string logString;
        public DateTime time;
        [TextArea] public string stacktrace;


        public LogItemData(LogType type, string logString, string stacktrace)
        {
            this.type = type;
            this.logString = logString;
            this.stacktrace = stacktrace;
            this.time = DateTime.UtcNow;
        }


        public string GetTimeToString()
        { 
            return string.Format("[{0}]", time.ToString("dd.MM.yyy HH:mm:ss"));
        }
    }
}
#endif
