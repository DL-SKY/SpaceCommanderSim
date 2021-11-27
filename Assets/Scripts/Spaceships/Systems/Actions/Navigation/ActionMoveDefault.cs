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
        public override EnumSpaceshipSystemActions Type => EnumSpaceshipSystemActions.MoveDefault;

        public ActionMoveDefault(Spaceship spaceship, SpaceshipSystem system) : base(spaceship, system)
        {
            //reserved
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
                UpdateRotation(fixedDeltaTime);
            }
        }

        public override void Execute(ActionData data)
        {
            //Debug.LogError($"Execute() {data.Type.ToString()}");

            SetState(EnumSpaceshipSystemActionStates.Started);
        }


        private void UpdatePosition(float fixedDeltaTime)
        {
            var speed = _spaceship.Parameters.GetCalculatedParameter(EnumSpaceshipParameters.Speed);
            var spaceshipRB = _spaceship.GetRigidbody();
            spaceshipRB.MovePosition(spaceshipRB.position + _spaceship.TransformSelf.forward * speed * fixedDeltaTime);
        }

        private void UpdateRotation(float fixedDeltaTime)
        {
            var spaceship = _spaceship.GetRigidbody();
            var eulerRot = spaceship.rotation.eulerAngles;
            var parameterZ = _spaceship.Parameters.GetCalculatedParameter(EnumSpaceshipParameters.RotationZ);
            var maneuver = _spaceship.Parameters.GetCalculatedParameter(EnumSpaceshipParameters.Maneuver);
            var defaultZ = Quaternion.AngleAxis(-eulerRot.z + parameterZ, Vector3.forward);            

            spaceship.rotation = Quaternion.RotateTowards(
                spaceship.rotation,
                spaceship.rotation * defaultZ,
                maneuver * fixedDeltaTime);
        }
    }
}
