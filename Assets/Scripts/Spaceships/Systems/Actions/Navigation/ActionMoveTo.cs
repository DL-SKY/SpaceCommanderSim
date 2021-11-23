using SCS.Enums;
using UnityEngine;

namespace SCS.Spaceships.Systems.Actions.Navigation
{
    public class ActionMoveToData : ActionData
    {
        public override EnumSpaceshipSystemActions Type => EnumSpaceshipSystemActions.MoveTo;

        public Transform target;
    }


    public class ActionMoveTo : Actions.Action
    {
        private ActionMoveToData _data;
        private float _waitTimer;


        public ActionMoveTo(Spaceship spaceship, SpaceshipSystem system) : base(spaceship, system)
        {
            //reserved
        }


        public override void DoUpdate(float deltaTime)
        {
            if (State == EnumSpaceshipSystemActionStates.Waiting)
            {
                _waitTimer -= deltaTime;
                if (_waitTimer <= 0.0f)
                    SetState(EnumSpaceshipSystemActionStates.Started);
            }
        }

        public override void DoFixedUpdate(float fixedDeltaTime)
        {
            if (State == EnumSpaceshipSystemActionStates.Started)
            {
                if (_data.target == null)
                    SetState(EnumSpaceshipSystemActionStates.Completed);

                //Вычисляем точку для поворота
                var targetRot = _data.target.position - _spaceship.TransformSelf.position;

                if (targetRot != Vector3.zero && Vector3.Angle(_spaceship.TransformSelf.forward, targetRot) > 0.25)
                {
                    //TODO - маневренность
                    _spaceship.GetRegidbody().rotation = 
                        Quaternion.RotateTowards(_spaceship.TransformSelf.rotation, 
                        Quaternion.FromToRotation(Vector3.forward, targetRot), 
                        180.0f * fixedDeltaTime);
                }
            }
        }

        public override void Execute(ActionData data)
        {
            _data = (ActionMoveToData)data;
            _waitTimer = _system.GenerateExecuteCommandPause(_data.systemSkillLevel, _data.executeCommandPauseCoeff);

            Debug.LogError($"Execute() {data.Type.ToString()} / pause {_waitTimer}");

            SetState(EnumSpaceshipSystemActionStates.Waiting);
        }
    }
}
