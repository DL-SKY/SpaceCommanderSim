using SCS.Constants;
using SCS.Enums;
using UnityEngine;

namespace SCS.Spaceships.Systems.Actions.Navigation
{
    public class ActionSpeedClampData : ActionData
    {
        public override EnumSpaceshipSystemActions Type => EnumSpaceshipSystemActions.SpeedClamp;

        public float targetNormalizeClampMod;
        public bool immediately;
    }

    public class ActionSpeedClamp : Actions.Action
    {
        public override EnumSpaceshipSystemActions Type => EnumSpaceshipSystemActions.SpeedClamp;

        private ActionSpeedClampData _data;


        public ActionSpeedClamp(Spaceship spaceship, SpaceshipSystem system, float startClampMod) : base(spaceship, system)
        {
            _spaceship.Parameters.SetMod(EnumSpaceshipParameters.Speed, ConstantsSpaceshipParametersMods.CLAMPED_SPEED, startClampMod);
        }


        public override void DoUpdate(float deltaTime)
        {
            switch (State)
            {
                case EnumSpaceshipSystemActionStates.Waiting:
                    _waitTimer -= deltaTime;
                    if (_waitTimer <= 0.0f)
                        SetState(EnumSpaceshipSystemActionStates.Started);
                    break;
                case EnumSpaceshipSystemActionStates.Started:
                    var clampedSpeedMod = _spaceship.Parameters.GetMod(EnumSpaceshipParameters.Speed, ConstantsSpaceshipParametersMods.CLAMPED_SPEED);
                    var accelerate = _spaceship.Parameters.GetCalculatedParameter(EnumSpaceshipParameters.AccelerateTime);
                    if (Mathf.Abs(clampedSpeedMod - _data.targetNormalizeClampMod) <= float.Epsilon)
                    {
                        SetState(EnumSpaceshipSystemActionStates.Completed);
                        return;
                    }
                    else
                    {
                        var newClampModValue = Mathf.MoveTowards(clampedSpeedMod, _data.targetNormalizeClampMod, 1 / accelerate * deltaTime);
                        _spaceship.Parameters.SetMod(EnumSpaceshipParameters.Speed, ConstantsSpaceshipParametersMods.CLAMPED_SPEED, newClampModValue);
                    }
                    break;
            }
        }

        public override void DoFixedUpdate(float fixedDeltaTime)
        {
            //reserved
        }

        public override void Execute(ActionData data)
        {
            _data = (ActionSpeedClampData)data;
            _waitTimer = _data.immediately ? 0.0f : _system.GenerateExecuteCommandPause(_data.systemSkillLevel, _data.executeCommandPauseCoeff);

            Debug.LogError($"Execute() {data.Type.ToString()} / pause {_waitTimer}");

            SetState(EnumSpaceshipSystemActionStates.Waiting);
        }
    }
}
