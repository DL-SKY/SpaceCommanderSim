using UnityEngine;
using UnityEngine.UI;

namespace DllSky.StarterKITv2.UI.Windows.FPS
{
    public class FPSWindow : WindowBase
    {
        public const string prefabPath = @"PrefabsStarterKIT\UI\Windows\FPS\FPSWindow";

        [Header("Link")]
        [SerializeField] private Text _fpsText;

        private float _deltaTime;
        //private float _avgFPS = 0;
        private float _newFPS = 0;
        //private int _frameCount = 0;
        //private float _minFPS = float.MaxValue;
        //private float _maxFPS = 0f;
        //private float[] _lastFPSProbes;
        //private int _probesArrayLength = 10;


        private void Awake()
        {
            ResetFPS();
        }

        private void Update()
        {
            UpdateFPS();
        }


        public void ResetFPS()
        {
            _deltaTime = 0;
            //_avgFPS = 0;
            _newFPS = 0;
            //_frameCount = 0;
            //_minFPS = float.MaxValue;
            //_maxFPS = 0f;

            //_lastFPSProbes = new float[_probesArrayLength];
        }


        private void UpdateFPS()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;

            _newFPS = (1.0f / _deltaTime);

            //frameCount++;
            //avgFPS += (newFPS - avgFPS) / frameCount;

            //if (frameCount < probesArrayLength)
            //    lastFPSProbes[frameCount] = newFPS;
            //else if (frameCount == probesArrayLength)
            //    maxFPS = Mathf.Min(lastFPSProbes);
            //else
            //    maxFPS = Mathf.Max(newFPS, maxFPS);

            //minFPS = Mathf.Min(newFPS, minFPS);

            //fpsText.text = string.Format("FPS : {0:0} \nMIN: {1:0} \nMAX: {2:0} \nAVG: {3:2}", newFPS, minFPS, maxFPS, avgFPS);

            _fpsText.text = string.Format("FPS : {0:0}", _newFPS);
        }
    }
}
