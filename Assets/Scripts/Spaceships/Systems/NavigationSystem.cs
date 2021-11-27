using SCS.Enums;
using SCS.ScriptableObjects.GlobalConfigs;
using SCS.Spaceships.Systems.Actions;
using SCS.Spaceships.Systems.Actions.Navigation;
using System.Collections.Generic;

namespace SCS.Spaceships.Systems
{
    public class NavigationSystem : SpaceshipSystem
    {
        public override EnumSpaceshipSystems System => EnumSpaceshipSystems.Navigation;


        public override void DoUpdate(float deltaTime)
        {
            foreach (var action in _actions)
                action.Value.DoUpdate(deltaTime);
        }

        public override void DoFixedUpdate(float fixedDeltaTime)
        {
            foreach (var action in _actions)
                action.Value.DoFixedUpdate(fixedDeltaTime);
        }

        public override void DoAction(ActionData data)
        {
            if (_actions.ContainsKey(data.Type))
                _actions[data.Type].Execute(data);
        }


        protected override void InitializeActionsDictionary()
        {
            SpaceshipNavigationSystemConfig navigationSystemSpecialConfig = null;
            try
            {
                navigationSystemSpecialConfig = (SpaceshipNavigationSystemConfig)_globalSystemConfig;
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError(e.Message + " => " + e.StackTrace);
                throw;
            }
            var moveDefaultAutoStart = navigationSystemSpecialConfig?.moveDafeultAutoStart ?? true;
            var speedChangeStartMod = navigationSystemSpecialConfig?.speedChangeStartMod ?? 0.0f;
            var speecClampStartMod = navigationSystemSpecialConfig?.speedClampStartMod ?? 0.0f;

            _actions = new Dictionary<Enums.EnumSpaceshipSystemActions, Actions.Action>
            {
                { Enums.EnumSpaceshipSystemActions.MoveTo, new ActionMoveTo(_spaceship, this) },
                { Enums.EnumSpaceshipSystemActions.MoveDefault, new ActionMoveDefault(_spaceship, this, autoStart: moveDefaultAutoStart) },
                { Enums.EnumSpaceshipSystemActions.SpeedChange, new ActionSpeedChange(_spaceship, this, startSpeedMod: speedChangeStartMod) },
                { Enums.EnumSpaceshipSystemActions.SpeedClamp, new ActionSpeedClamp(_spaceship, this, speecClampStartMod) },
            };
        }

        protected override void OnActionStateChangeHandler(EnumSpaceshipSystemActions type, EnumSpaceshipSystemActionStates prevState, EnumSpaceshipSystemActionStates state)
        {
            //UnityEngine.Debug.LogError($"OnActionStateChangeHandler({type.ToString()}, {prevState.ToString()}, {state.ToString()})");
        }
    }
}
