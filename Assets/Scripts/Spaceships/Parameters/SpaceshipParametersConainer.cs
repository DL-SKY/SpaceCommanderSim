using DllSky.StarterKITv2.Services;
using SCS.Application;
using SCS.Datas.Spaceships;
using SCS.Enums;
using SCS.ScriptableObjects.GlobalConfigs;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Spaceships.Parameters
{
    public class SpaceshipParametersContainer
    {
        public Dictionary<EnumSpaceshipParameters, float> parametersBase = new Dictionary<EnumSpaceshipParameters, float>();
        public Dictionary<EnumSpaceshipParameters, float> parametersMods = new Dictionary<EnumSpaceshipParameters, float>();    //TODO: переделать float на другой тип, наверно нужна будет коллекция

        private SkillsConfig _globalSkillsConfig;


        public void Initialize(SpaceshipData data)
        {
            var configManager = ComponentLocator.Resolve<GlobalConfigManager>();
            _globalSkillsConfig = configManager.GetSkillsConfig();

            //TODO (будет переделано после внедрения модулей)
            FillDictionaryParametersBase(data);
        }

        public float GetParameter(EnumSpaceshipParameters parameter)
        {
            //TODO: добавить учет модификаторов (контроль текущей скорости тоже через модификаторы)

            if (parametersBase.ContainsKey(parameter))
                return parametersBase[parameter];
            else
                return 0.0f;
        }


        private void FillDictionaryParametersBase(SpaceshipData data)
        {
            foreach (var parameter in data.parameters)
            {
                //TODO skilLVL => maxSystemSkillLevel
                var maxSystemSkillLevel = 1;
                var t = Mathf.InverseLerp(_globalSkillsConfig.levelMin, _globalSkillsConfig.levelMax, maxSystemSkillLevel);
                var currentSkillLevel = Mathf.Lerp(parameter.range.x, parameter.range.y, t);
                parametersBase.Add(parameter.parameter, currentSkillLevel);
            }
        }
    }
}
