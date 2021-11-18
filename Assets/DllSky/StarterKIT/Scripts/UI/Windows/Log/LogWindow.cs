#if LOGGING
using DllSky.StarterKITv2.Logging;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace DllSky.StarterKITv2.UI.Windows.Log
{
    public class LogWindow : WindowBase
    {
        public const string prefabPath = @"PrefabsStarterKIT\UI\Windows\Log\ExampleLogWindow";

        [Space()]
        [SerializeField] private GameObject _logItemPrefab;
        [SerializeField] private ScrollRect _scroll;

#if LOGGING
        private LogManager _logManager;
#endif


        private void Awake()
        {
#if LOGGING
            _logManager = Services.ComponentLocator.Resolve<LogManager>();
#endif
        }

        private void OnDestroy()
        {
            for (int i = _scroll.content.childCount - 1; i >= 0; i--)
                Destroy(_scroll.content.GetChild(i));
        }


        public override void Initialize(object data)
        {
#if LOGGING
            if (_logManager == null)
                return;

            var datas = _logManager.GetLogs();
            foreach (var logData in datas)
            {
                var log = Instantiate(_logItemPrefab, _scroll.content).GetComponent<LogItem>();
                log.Initialize(logData);
            }    
#endif
        }
    }
}
