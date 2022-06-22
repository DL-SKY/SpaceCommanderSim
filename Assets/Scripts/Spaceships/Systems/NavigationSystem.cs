using SCS.Enums;
using SCS.ScriptableObjects.GlobalConfigs;
using SCS.Spaceships.Systems.Actions.Navigation;
using System.Collections.Generic;

namespace SCS.Spaceships.Systems
{
    public class NavigationSystem : SpaceshipSystem
    {
        public override EnumSpaceshipSystems System => EnumSpaceshipSystems.Navigation;


        protected override void InitializeActionsDictionary()
        {
            SpaceshipNavigationSystemConfig navigationSystemSpecialConfig = TryCastingSpaceshipSystemConnfig<SpaceshipNavigationSystemConfig>(_globalSystemConfig);
            var speedChangeStartMod = navigationSystemSpecialConfig?.speedChangeStartMod ?? 0.0f;
            var speecClampStartMod = navigationSystemSpecialConfig?.speedClampStartMod ?? 0.0f;

            _actions = new Dictionary<Enums.EnumSpaceshipSystemActions, Actions.Action>
            {
                { Enums.EnumSpaceshipSystemActions.MoveTo, new ActionMoveTo(_spaceship, this) },
                { Enums.EnumSpaceshipSystemActions.MoveDefault, new ActionMoveDefault(_spaceship, this) },
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
