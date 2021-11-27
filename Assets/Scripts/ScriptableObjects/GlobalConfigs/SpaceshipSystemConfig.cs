using UnityEngine;

namespace SCS.ScriptableObjects.GlobalConfigs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GlobalConfigs/SpaceshipSystemConfig", fileName = "SpaceshipSystemConfig")]
    public class SpaceshipSystemConfig : ScriptableObject
    {
        [Header("Execute Command Pause")]
        public Vector2 executePauseMin;
        public Vector2 executePauseMax;
    }

    [CreateAssetMenu(menuName = "ScriptableObjects/GlobalConfigs/SpaceshipNavigationSystemConfig", fileName = "NavigationSystemConfig")]
    public class SpaceshipNavigationSystemConfig : SpaceshipSystemConfig
    {
        [Header("ActionMoveDefault Settings")]
        public bool moveDafeultAutoStart;

        [Header("ActionSpeedChange Settings")]
        [Range(0.0f, 1.0f)] public float speedChangeStartMod;

        [Header("ActionSpeedClamp Settings")]
        [Range(0.0f, 1.0f)] public float speedClampStartMod;
    }
}
