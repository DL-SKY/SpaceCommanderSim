using SCS.Enums;
using UnityEngine;

namespace SCS.ScriptableObjects.GlobalConfigs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GlobalConfigs/SpaceshipSystemConfig", fileName = "SpaceshipSystemConfig")]
    public class SpaceshipSystemConfig : ScriptableObject
    {
        [Header("Execute Command Pause")]
        public Vector2 executePauseMin;
        public Vector2 executePauseMax;

        [Header("Autostart")]
        public EnumSpaceshipSystemActions[] autoStartActions;
    }
}
