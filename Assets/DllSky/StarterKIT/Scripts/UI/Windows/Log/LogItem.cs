#if LOGGING

using DllSky.StarterKITv2.Logging;
using UnityEngine;
using UnityEngine.UI;

namespace DllSky.StarterKITv2.UI.Windows.Log
{
    public class LogItem : MonoBehaviour
    {
        [SerializeField] private Image _indication;
        [SerializeField] private Text _log;
        [SerializeField] private Text _time;
        private LogItemData _data;


        private void OnDestroy()
        {
            _data = null;
        }


        public void Initialize(LogItemData data)
        {
            _data = data;

            _indication.color = GetColor(_data.type);
            _log.text = _data.logString;
            _time.text = _data.GetTimeToString();
        }


        private Color GetColor(LogType type)
        {
            var result = Color.white;

            switch (type)
            {
                case LogType.Warning:
                    result = Color.yellow;
                    break;
                case LogType.Error:
                    result = Color.red;
                    break;
            }

            return result;
        }
    }
}
#endif
