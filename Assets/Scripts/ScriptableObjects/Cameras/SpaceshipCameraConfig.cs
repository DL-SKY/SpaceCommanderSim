using UnityEngine;

namespace SCS.ScriptableObjects.Cameras
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Cameras/SpaceshipCameraConfig", fileName = "SpaceshipCameraConfig")]
    public class SpaceshipCameraConfig : ScriptableObject
    {
        [Header("Zoom")]
        public bool useZoom;
        public float zoomSensitivity = 1.0f;
        public bool zoomClamp = true;
        public float zoomMin = 10.0f;
        public float zoomMax = 60.0f;
        public float zoomDefault = 45.0f;

        [Header("Pitch")]
        public bool usePitch;        
        public float pitchSensitivity = 0.25f;
        public bool pitchClamp = true;
        public float pitchMin = -90.0f;
        public float pitchMax = 90.0f;
        public float pitchDefault = 0.0f;

        [Header("Yaw")]
        public bool useYaw;        
        public float yawSensitivity = 0.25f;
        public bool yawClamp = false;
        public float yawMin = -45.0f;
        public float yawMax = 45.0f;
        public float yawDefault = 0.0f;

        [Header("Any Settings")]
        public float fieldOfViewDefault = 60.0f;
    }
}
