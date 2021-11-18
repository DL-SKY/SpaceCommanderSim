using DllSky.StarterKITv2.Constants;
using DllSky.StarterKITv2.Events;
using UnityEngine;

namespace DllSky.StarterKITv2.Tools.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraConstantWidth : MonoBehaviour
    {
        [SerializeField] private Vector2 _defaultResolution = new Vector2(1920, 1080);
        [Range(0f, 1f)]
        [SerializeField] private float _widthOrHeight = 0.0f;

        private UnityEngine.Camera _camera;

        private float _initialSize;
        private float _targetAspect;

        private float _initialFov;
        private float _horizontalFov = 120.0f;


        private void Start()
        {
            EventManager.AddEventListener(ConstantEventsName.ON_RESOLUTION_CHANGE, OnResolutionChangeHandler);

            Initialize();
            ApplySettings();
        }

        private void OnDestroy()
        {
            EventManager.RemoveEventListener(ConstantEventsName.ON_RESOLUTION_CHANGE, OnResolutionChangeHandler);
        }


        public void ApplySettings()
        {
            if (_camera.orthographic)
            {
                float constantWidthSize = _initialSize * (_targetAspect / _camera.aspect);
                _camera.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, _widthOrHeight);
            }
            else
            {
                float constantWidthFov = CalcVerticalFov(_horizontalFov, _camera.aspect);
                _camera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, _widthOrHeight);
            }
        }


        private void Initialize()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _initialSize = _camera.orthographicSize;

            _targetAspect = _defaultResolution.x / _defaultResolution.y;

            _initialFov = _camera.fieldOfView;
            _horizontalFov = CalcVerticalFov(_initialFov, 1.0f / _targetAspect);
        }        

        private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
        {
            float hFovInRads = hFovInDeg * Mathf.Deg2Rad;
            float vFovInRads = 2.0f * Mathf.Atan(Mathf.Tan(hFovInRads / 2.0f) / aspectRatio);

            return vFovInRads * Mathf.Rad2Deg;
        }

        private void OnResolutionChangeHandler(CustomEvent e)
        {
            ApplySettings();
        }
    }
}
