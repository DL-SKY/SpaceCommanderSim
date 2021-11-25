using SCS.Enums;
using UnityEngine;

namespace SCS.Spaceships.Systems.Actions.Navigation
{
    public class ActionMoveDefaultData : ActionData
    {
        public override EnumSpaceshipSystemActions Type => EnumSpaceshipSystemActions.MoveDefault;
    }

    public class ActionMoveDefault : Actions.Action
    {
        public ActionMoveDefault(Spaceship spaceship, SpaceshipSystem system, bool autoStart) : base(spaceship, system)
        {
            if (autoStart)
                Execute(new ActionMoveDefaultData());            
        }


        public override void DoUpdate(float deltaTime)
        {
            //reserved
        }

        public override void DoFixedUpdate(float fixedDeltaTime)
        {
            if (State == EnumSpaceshipSystemActionStates.Started)
            {
                UpdatePosition(fixedDeltaTime);
            }
        }

        public override void Execute(ActionData data)
        {
            Debug.LogError($"Execute() {data.Type.ToString()}");

            SetState(EnumSpaceshipSystemActionStates.Started);
        }


        private void UpdatePosition(float fixedDeltaTime)
        {
            var speed = _spaceship.Parameters.GetCalculatedParameter(EnumSpaceshipParameters.Speed);
            var spaceshipRB = _spaceship.GetRigidbody();
            spaceshipRB.MovePosition(spaceshipRB.position + _spaceship.TransformSelf.forward * speed * fixedDeltaTime);
        }
    }
}
