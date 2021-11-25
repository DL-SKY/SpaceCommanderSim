using SCS.Enums;
using System;
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
        private ActionMoveToData _data;
        private float _waitTimer;

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
            _sqrCalcMinDistanceRadius = (selfMinDistanceRadius + targetMinDistanceRadius) * (selfMinDistanceRadius + targetMinDistanceRadius);

            Debug.LogError($"Execute() {data.Type.ToString()} / pause {_waitTimer}");

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

            if (targetRot != Vector3.zero && Vector3.Angle(_spaceship.TransformSelf.forward, targetRot) > 0.25)
            {
                _spaceship.GetRigidbody().rotation = Quaternion.RotateTowards(
                    _spaceship.TransformSelf.rotation,
                    Quaternion.FromToRotation(Vector3.forward, targetRot),
                    _spaceship.Parameters.GetCalculatedParameter(EnumSpaceshipParameters.Maneuver) * fixedDeltaTime);
            }
        }

        [Obsolete]
        private void UpdatePosition(float fixedDeltaTime)
        {
            //TODO - разгон/торможение/скорость
            var speed = _spaceship.Parameters.GetCalculatedParameter(EnumSpaceshipParameters.Speed);

            //Проверка на дистанцию. В случае необходимости останавливаем корабль
            //var distance = Vector3.Distance(transform.position, point.position);
            //if (distance > brakingDistance && distance > brakingDistanceSpeedType)
            //    SetSpeedNormalize(GetMaxSpeedForCurrentSpeedType());
            //else if (meta.GetSpeedResultNormalize() > 0.0f)
            //    SetSpeedNormalize(0.0f);

            var spaceshipRB = _spaceship.GetRigidbody();
            spaceshipRB.MovePosition(spaceshipRB.position + _spaceship.TransformSelf.forward * speed * fixedDeltaTime);
        }

        private void UpdateSpeed()
        {            
            var sqrDistance = (_data.target.position - _spaceship.TransformSelf.position).sqrMagnitude;
            
            //TODO
            //...

            //var newSpeedMod = 1.0f;
            //DoActionSpeedChange(newSpeedMod);
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
    }
}
