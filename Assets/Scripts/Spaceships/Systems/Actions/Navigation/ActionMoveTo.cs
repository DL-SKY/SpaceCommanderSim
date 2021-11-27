using SCS.Enums;
using UnityEngine;

namespace SCS.Spaceships.Systems.Actions.Navigation
{
    public class ActionMoveToData : ActionData
    {
        public override EnumSpaceshipSystemActions Type => EnumSpaceshipSystemActions.MoveTo;

        public Transform target;
        public float targetMinDistanceRadius;
    }


    public class ActionMoveTo : Actions.Action
    {
        public override EnumSpaceshipSystemActions Type => EnumSpaceshipSystemActions.MoveTo;

        private ActionMoveToData _data;

        private float _calcMinDistanceRadius;
        private float _sqrCalcMinDistanceRadius;


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

                UpdateRotation(fixedDeltaTime);                
                UpdateSpeed();
            }
        }

        public override void Execute(ActionData data)
        {
            _data = (ActionMoveToData)data;
            _waitTimer = _system.GenerateExecuteCommandPause(_data.systemSkillLevel, _data.executeCommandPauseCoeff);

            var selfMinDistanceRadius = _spaceship.MinDistanceRadius;
            var targetMinDistanceRadius = _data.targetMinDistanceRadius;
            _calcMinDistanceRadius = selfMinDistanceRadius + targetMinDistanceRadius;
            _sqrCalcMinDistanceRadius = _calcMinDistanceRadius * _calcMinDistanceRadius;

            Debug.LogError($"Execute() {data.Type.ToString()} / pause {_waitTimer} / minDist {_calcMinDistanceRadius} - {_sqrCalcMinDistanceRadius}");

            SetState(EnumSpaceshipSystemActionStates.Waiting);

            if (!_system.CheckActiveAction(EnumSpaceshipSystemActions.MoveDefault))
                StartActionMoveDefault();
        }


        private void StartActionMoveDefault()
        {
            _system.DoAction(new ActionMoveDefaultData());
        }

        private void UpdateRotation(float fixedDeltaTime)
        {
            var targetRot = _data.target.position - _spaceship.TransformSelf.position;

            if (targetRot != Vector3.zero && Vector3.Angle(_spaceship.TransformSelf.forward, targetRot) > 0.25f)
            {
                var maneuver = _spaceship.Parameters.GetCalculatedParameter(EnumSpaceshipParameters.Maneuver);
                Quaternion targetQuaternion = Quaternion.RotateTowards(_spaceship.TransformSelf.rotation,
                                                        Quaternion.LookRotation(targetRot),
                                                        maneuver * fixedDeltaTime);
                _spaceship.GetRigidbody().rotation = targetQuaternion;
            }
        }

        private void UpdateSpeed()
        {            
            var sqrDistance = (_data.target.position - _spaceship.TransformSelf.position).sqrMagnitude;
            var speed = _spaceship.Parameters.GetCalculatedParameter(EnumSpaceshipParameters.Speed);

            var newSpeedMod = 0.0f;
            if (sqrDistance > _sqrCalcMinDistanceRadius && sqrDistance > GetSqrDistanceBraking(_sqrCalcMinDistanceRadius, _calcMinDistanceRadius, speed))
                newSpeedMod = 1.0f;
            else if (sqrDistance <= _sqrCalcMinDistanceRadius)
                SetState(EnumSpaceshipSystemActionStates.Completed);

            DoActionSpeedChange(newSpeedMod);
        }

        private void DoActionSpeedChange(float newSpeedMod)
        {
            if (_system.CheckWaitingAction(EnumSpaceshipSystemActions.SpeedChange) || _system.CheckActiveAction(EnumSpaceshipSystemActions.SpeedChange))
            {
                var actionSpeedChange = (ActionSpeedChange)_system.GetAction(EnumSpaceshipSystemActions.SpeedChange);
                actionSpeedChange.TryUpdateTargetNormalizeSpeedMod(newSpeedMod, true);
            }
            else
            {
                _system.DoAction(new ActionSpeedChangeData { targetNormalizeSpeedMod = newSpeedMod, immediately = true, });
            }
        }

        private float GetSqrDistanceBraking(float sqrCalcMinDistance, float calcMinDistance, float speed)
        {
            var accelerate = _spaceship.Parameters.GetCalculatedParameter(EnumSpaceshipParameters.AccelerateTime);
            var brakingTime = speed / accelerate;
            var brakingDistance = brakingTime * speed;

            return sqrCalcMinDistance + (2 * calcMinDistance * brakingDistance) + (brakingDistance * brakingDistance);  //квадрат суммы
        }
    }
}
