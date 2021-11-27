﻿using DllSky.StarterKITv2.Services;
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


        protected void OnDestroy()
        {
            Unsubscribe();
        }


        public void Initialize(Spaceship spaceship)
        {
            var configManager = ComponentLocator.Resolve<GlobalConfigManager>();
            _globalSkillsConfig = configManager.GetSkillsConfig();
            _globalSystemConfig = configManager.GetSpaceshipSystemConfig(System);

            _spaceship = spaceship;
            InitializeActionsDictionary();

            Subscribe();
        }

        public float GenerateExecuteCommandPause(int skillLevel, float coeff = 1.0f)
        {
            var t = Mathf.InverseLerp(_globalSkillsConfig.levelMin, _globalSkillsConfig.levelMax, skillLevel);
            var min = Mathf.Lerp(_globalSystemConfig.executePauseMin.x, _globalSystemConfig.executePauseMin.y, t) * coeff;
            var max = Mathf.Lerp(_globalSystemConfig.executePauseMax.x, _globalSystemConfig.executePauseMax.y, t) * coeff;

            return Random.Range(min, max);
        }

        public Actions.Action GetAction(EnumSpaceshipSystemActions type)
        {
            if (_actions.ContainsKey(type))
                return _actions[type];
            else
                return null;
        }

        public bool CheckActiveAction(EnumSpaceshipSystemActions type)
        {
            var action = GetAction(type);
            if (action != null)
                return action.State == EnumSpaceshipSystemActionStates.Started;
            else
                return false;
        }

        public bool CheckWaitingAction(EnumSpaceshipSystemActions type)
        {
            var action = GetAction(type);
            if (action != null)
                return action.State == EnumSpaceshipSystemActionStates.Waiting;
            else
                return false;
        }


        protected void Subscribe()
        {
            foreach (var action in _actions)
                action.Value.OnStateChange += OnActionStateChangeHandler;
        }

        protected void Unsubscribe()
        {
            foreach (var action in _actions)
                action.Value.OnStateChange -= OnActionStateChangeHandler;
        }


        public abstract void DoUpdate(float deltaTime);
        public abstract void DoFixedUpdate(float fixedDeltaTime);
        public abstract void DoAction(ActionData data);

        protected abstract void InitializeActionsDictionary();
        protected abstract void OnActionStateChangeHandler(EnumSpaceshipSystemActions type, EnumSpaceshipSystemActionStates prevState, EnumSpaceshipSystemActionStates state);
    }
}
