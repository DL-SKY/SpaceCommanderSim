using UnityEngine;

namespace SCS.ScriptableObjects.GlobalConfigs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GlobalConfigs/SpaceshipNavigationSystemConfig", fileName = "NavigationSystemConfig")]
    public class SpaceshipNavigationSystemConfig : SpaceshipSystemConfig
    {
        [Header("ActionSpeedChange Settings")]
        [Range(0.0f, 1.0f)] public float speedChangeStartMod;

        [Header("ActionSpeedClamp Settings")]
        [Range(0.0f, 1.0f)] public float speedClampStartMod;
    }
}
