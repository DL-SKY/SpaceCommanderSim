using DllSky.StarterKITv2.Tools.Components;
using Lean.Touch;
using SCS.ScriptableObjects.Cameras;
using UnityEngine;

namespace SCS.Cameras
{
    public class SpaceshipCameraController : AutoLocatorObject
    {
        [Header("Settings")]
        [SerializeField] private Camera _camera;
        [SerializeField] private bool _ignoreStartedOverGui = true;
        [SerializeField] private bool _ignoreIsOverGui;

        [Header("Config")]
        [SerializeField] private SpaceshipCameraConfig _config;

        [Header("Current Values")]
        [SerializeField] private float _zoom;
        [SerializeField] private float _zoomOffset;
        [SerializeField] private float _pitch;
        [SerializeField] private float _pitchOffset;
        [SerializeField] private float _yaw;
        [SerializeField] private float _yawOffset;
        [SerializeField] private float _fov;
        [SerializeField] private float _fovOffset;

        private Transform _cameraTransform;
        private Transform _transform;


        protected override void CustomAwake()
        {
            if (_camera == null)
                _camera = Camera.main;
            _cameraTransform = _camera.transform;

            _transform = transform;

            if (_config == null)
                Debug.LogError("[SpaceshipCameraController] CameraConfig is NULL");

            ApplyDefaultValues();
        }

        private void LateUpdate()
        {
            var fingers = LeanSelectable.GetFingers(_ignoreStartedOverGui, _ignoreIsOverGui);

            var pinchRatio = LeanGesture.GetPinchRatio(fingers);
            _zoom = GetCalculatedZoom(pinchRatio);
            _zoomOffset = GetCalculatedZoomOffset();
            SetZoom(_zoom + _zoomOffset);

            var drag = LeanGesture.GetScaledDelta(fingers);
            _pitch = GetCalculatedPitch(drag.y);
            _pitchOffset = GetCalculatedPitchOffset();
            _yaw = GetCalculatedYaw(drag.x);
            _yawOffset = GetCalculatedYawOffset();
            SetPitchAndYaw(_pitch + _pitchOffset, _yaw + _yawOffset);

            _fov = GetCalculatedFOV();
            _fovOffset = GetCalculatedFOVOffset();
            SetFOV(_fov + _fovOffset);
        }


        private void ApplyDefaultValues()
        {
            _zoom = _config.zoomDefault;
            _pitch = _config.pitchDefault;
            _yaw = _config.yawDefault;
            _fov = _config.fieldOfViewDefault;
        }

        private float GetCalculatedZoom(float pinchRatio)
        {
            var zoom = _zoom;

            if (_config.useZoom)
            {
                zoom *= (pinchRatio * _config.zoomSensitivity);
                if (_config.zoomClamp)
                    zoom = Mathf.Clamp(zoom, _config.zoomMin, _config.zoomMax);
            }

            return zoom;
        }

        private float GetCalculatedZoomOffset()
        {
            return _zoomOffset;
        }

        private void SetZoom(float newZoom)
        {
            _cameraTransform.localPosition = new Vector3(_cameraTransform.localPosition.x, _cameraTransform.localPosition.y, -newZoom);
        }

        private float GetCalculatedPitch(float dragY)
        {
            var pitch = _pitch;

            if (_config.usePitch)
            {
                pitch += (dragY * _config.pitchSensitivity);
                if (_config.pitchClamp)
                    pitch = Mathf.Clamp(pitch, _config.pitchMin, _config.pitchMax);
            }

            return pitch;
        }

        private float GetCalculatedPitchOffset()
        {
            return _pitchOffset;
        }

        private float GetCalculatedYaw(float dragX)
        { 
            var yaw = _yaw;

            if (_config.useYaw)
            {
                yaw -= (dragX * _config.yawSensitivity);
                if (_config.yawClamp)
                    yaw = Mathf.Clamp(yaw, _config.yawMin, _config.yawMax);
            }

            return yaw;
        }

        private float GetCalculatedYawOffset()
        {
            return _yawOffset;
        }

        private void SetPitchAndYaw(float newPitch, float newYaw)
        {
            _transform.localRotation = Quaternion.Euler(-newPitch, -newYaw, _transform.localRotation.z);
        }

        private float GetCalculatedFOV()
        {
            return _config.fieldOfViewDefault;
        }

        private float GetCalculatedFOVOffset()
        {
            return _fovOffset;
        }

        private void SetFOV(float newFOV)
        {
            _camera.fieldOfView = newFOV;
        }
    }
}
