using SCS.Enums;
using SCS.ScriptableObjects.GlobalConfigs;
using System;

namespace SCS.Datas.GlobalConfigs
{
    [Serializable]
    public class SpaceshipSystemConfigDictionaryData
    {
        public EnumSpaceshipSystems system;
        public SpaceshipSystemConfig config;
    }
}
