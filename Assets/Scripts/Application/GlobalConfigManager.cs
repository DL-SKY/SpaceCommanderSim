using DllSky.StarterKITv2.Tools.Components;
using SCS.Datas.GlobalConfigs;
using SCS.Enums;
using SCS.ScriptableObjects.GlobalConfigs;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Application
{
    public class GlobalConfigManager : AutoLocatorObject
    {
        [Space(25)]
        [SerializeField] private SkillsConfig _skillsConfig;
        [Space()]
        [SerializeField] private SpaceshipSystemConfigDictionaryData[] _spaceshipSystemsConfigsArray;
        private Dictionary<EnumSpaceshipSystems, SpaceshipSystemConfig> _spaceshipSystemsConfigs;


        protected override void CustomAwake()
        {
            CreateSystemsConfigsDictionary(_spaceshipSystemsConfigsArray);
        }


        public SkillsConfig GetSkillsConfig()
        {
            return _skillsConfig;
        }

        public SpaceshipSystemConfig GetSpaceshipSystemConfig(EnumSpaceshipSystems system)
        {
            if (_spaceshipSystemsConfigs.ContainsKey(system))
                return _spaceshipSystemsConfigs[system];
            else
                return new SpaceshipSystemConfig();
        }


        private void CreateSystemsConfigsDictionary(SpaceshipSystemConfigDictionaryData[] array)
        {
            _spaceshipSystemsConfigs = new Dictionary<EnumSpaceshipSystems, SpaceshipSystemConfig>();

            if (array == null)
                return;

            foreach (var data in array)
                if (!_spaceshipSystemsConfigs.ContainsKey(data.system))
                    _spaceshipSystemsConfigs.Add(data.system, data.config);
        }
    }
}
