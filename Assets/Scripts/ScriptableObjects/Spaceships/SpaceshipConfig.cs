using SCS.Enums;
using UnityEngine;

namespace SCS.ScriptableObjects.Cameras
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Spaceships/SpaceshipConfig", fileName = "SpaceshipConfig")]
    public class SpaceshipConfig : ScriptableObject
    {
        public EnumSpaceObjectSize size;
    }
}
