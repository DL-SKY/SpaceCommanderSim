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
        private Dictionary<EnumSpaceshipParameters, float> _parametersBase = new Dictionary<EnumSpaceshipParameters, float>();
        private Dictionary<EnumSpaceshipParameters, Dictionary<string, float>> _parametersMods = new Dictionary<EnumSpaceshipParameters, Dictionary<string, float>>();

        private SkillsConfig _globalSkillsConfig;


        public void Initialize(SpaceshipData data)
        {
            var configManager = ComponentLocator.Resolve<GlobalConfigManager>();
            _globalSkillsConfig = configManager.GetSkillsConfig();

            //TODO (будет переделано после внедрения модулей)
            FillDictionaryParametersBase(data);
        }

        public float GetCalculatedParameter(EnumSpaceshipParameters parameter)
        {
            if (_parametersBase.ContainsKey(parameter))
                return _parametersBase[parameter] * GetCalculateMods(parameter);
            else
                return 0.0f;
        }

        public float GetMod(EnumSpaceshipParameters parameter, string mod)
        {
            if (_parametersMods.ContainsKey(parameter))
            {
                if (_parametersMods[parameter].ContainsKey(mod))
                    return _parametersMods[parameter][mod];
                else
                    return 1.0f;
            }
            else
            {
                return 1.0f;
            }
        }

        public void SetMod(EnumSpaceshipParameters parameter, string mod, float value)
        {
            if (!_parametersMods.ContainsKey(parameter))
                _parametersMods.Add(parameter, new Dictionary<string, float>());

            if (!_parametersMods[parameter].ContainsKey(mod))
                _parametersMods[parameter].Add(mod, value);
            else
                _parametersMods[parameter][mod] = value;
        }


        private void FillDictionaryParametersBase(SpaceshipData data)
        {
            foreach (var parameter in data.parameters)
            {
                //TODO skilLVL => maxSystemSkillLevel
                var maxSystemSkillLevel = 1;
                var t = Mathf.InverseLerp(_globalSkillsConfig.levelMin, _globalSkillsConfig.levelMax, maxSystemSkillLevel);
                var currentSkillLevel = Mathf.Lerp(parameter.range.x, parameter.range.y, t);
                _parametersBase.Add(parameter.parameter, currentSkillLevel);
            }
        }

        private void FillDictionaryParametersMods(SpaceshipData data)
        { 
            //TODO
            //reserved
        }

        private float GetCalculateMods(EnumSpaceshipParameters parameter)
        {
            var mods = 1.0f;
            if (_parametersMods.ContainsKey(parameter))
                foreach (var mod in _parametersMods[parameter])
                    mods *= mod.Value;

            return mods;
        }
    }
}
