using DllSky.StarterKITv2.Services;
using SCS.Application;
using SCS.Enums;
using SCS.Interfaces.ECS;
using SCS.ScriptableObjects.GlobalConfigs;
using SCS.Spaceships.Systems.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Spaceships.Systems
{
    public abstract class SpaceshipSystem : MonoBehaviour, IUpdateSystem, IFixedUpdateSystem
    {
        public abstract EnumSpaceshipSystems System { get; }

        protected SkillsConfig _globalSkillsConfig;
        protected SpaceshipSystemConfig _globalSystemConfig;
        protected Spaceship _spaceship;
        protected Dictionary<Enums.EnumSpaceshipSystemActions, Actions.Action> _actions;


        public void Initialize(Spaceship spaceship)
        {
            var configManager = ComponentLocator.Resolve<GlobalConfigManager>();
            _globalSkillsConfig = configManager.GetSkillsConfig();
            _globalSystemConfig = configManager.GetSpaceshipSystemConfig(System);

            _spaceship = spaceship;
            InitializeActionsDictionary();
        }

        public float GenerateExecuteCommandPause(int skillLevel, float coeff = 1.0f)
        {
            var t = Mathf.InverseLerp(_globalSkillsConfig.levelMin, _globalSkillsConfig.levelMax, skillLevel);
            var min = Mathf.Lerp(_globalSystemConfig.executePauseMin.x, _globalSystemConfig.executePauseMin.y, t) * coeff;
            var max = Mathf.Lerp(_globalSystemConfig.executePauseMax.x, _globalSystemConfig.executePauseMax.y, t) * coeff;

            return Random.Range(min, max);
        }

        
        public abstract void DoUpdate(float deltaTime);
        public abstract void DoFixedUpdate(float fixedDeltaTime);
        public abstract void DoAction(ActionData data);

        protected abstract void InitializeActionsDictionary();
    }
}
