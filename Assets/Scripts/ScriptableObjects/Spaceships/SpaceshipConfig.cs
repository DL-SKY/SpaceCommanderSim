using SCS.Enums;
using UnityEngine;

namespace SCS.ScriptableObjects.Spaceships
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Spaceships/SpaceshipConfig", fileName = "SpaceshipConfig")]
    public class SpaceshipConfig : ScriptableObject
    {
        public EnumSpaceObjectSize size;
    }
}
