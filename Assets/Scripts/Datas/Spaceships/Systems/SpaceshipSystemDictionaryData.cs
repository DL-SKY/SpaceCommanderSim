using SCS.Enums;
using SCS.Spaceships.Systems;
using System;

namespace SCS.Datas.Spaceships.Systems
{
    [Serializable]
    public class SpaceshipSystemDictionaryData
    {
        public EnumSpaceshipSystems type;
        public SpaceshipSystem link;
    }
}
