using SCS.Enums;
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
            _actions = new Dictionary<Enums.EnumSpaceshipSystemActions, Actions.Action>
            {
                { Enums.EnumSpaceshipSystemActions.MoveTo, new ActionMoveTo(_spaceship, this) },
            };
        }
    }
}
