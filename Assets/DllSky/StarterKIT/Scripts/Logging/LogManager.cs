using DllSky.StarterKITv2.Tools.Components;
#if LOGGING
using System.Collections.Generic;
using UnityEngine;
#endif

namespace DllSky.StarterKITv2.Logging
{
    public class LogManager : AutoLocatorObject
    {
#if LOGGING
        [SerializeField] private List<LogItemData> _logs = new List<LogItemData>();


        protected override void CustomAwake()
        {
            UnityEngine.Application.logMessageReceived += HandleLog;
        }

        protected override void CustomOnDestroy()
        {
            UnityEngine.Application.logMessageReceived -= HandleLog;
        }


        public void AddManualLog(string logString, string stackTrace, LogType type)
        {
            _logs.Add(new LogItemData(type, logString, stackTrace));
        }

        public void Clear()
        {
            _logs.Clear();
        }

        public LogItemData[] GetLogs()
        {
            return _logs.ToArray();
        }


        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            AddManualLog(logString, stackTrace, type);
        }
#endif
    }
}
