using SCS.Constants;
using SCS.Enums;
using UnityEngine;

namespace SCS.Spaceships.Systems.Actions.Navigation
{
    public class ActionSpeedChangeData : ActionData
    {
        public override EnumSpaceshipSystemActions Type => EnumSpaceshipSystemActions.SpeedChange;

        public float targetNormalizeSpeedMod;
        public bool immediately;
    }

    public class ActionSpeedChange : Actions.Action
    {
        public override EnumSpaceshipSystemActions Type => EnumSpaceshipSystemActions.SpeedChange;

        private ActionSpeedChangeData _data;


        public ActionSpeedChange(Spaceship spaceship, SpaceshipSystem system, float startSpeedMod) : base(spaceship, system)
        {
            _spaceship.Parameters.SetMod(EnumSpaceshipParameters.Speed, ConstantsSpaceshipParametersMods.CURRENT_SPEED, startSpeedMod);
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
                    var currentSpeedMod = _spaceship.Parameters.GetMod(EnumSpaceshipParameters.Speed, ConstantsSpaceshipParametersMods.CURRENT_SPEED);
                    var accelerate = _spaceship.Parameters.GetCalculatedParameter(EnumSpaceshipParameters.AccelerateTime);
                    if (Mathf.Abs(currentSpeedMod - _data.targetNormalizeSpeedMod) <= float.Epsilon)
                    {
                        SetState(EnumSpaceshipSystemActionStates.Completed);
                        return;
                    }
                    else
                    {
                        var newModValue = Mathf.MoveTowards(currentSpeedMod, _data.targetNormalizeSpeedMod, 1 / accelerate * deltaTime);
                        _spaceship.Parameters.SetMod(EnumSpaceshipParameters.Speed, ConstantsSpaceshipParametersMods.CURRENT_SPEED, newModValue);
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
            _data = (ActionSpeedChangeData)data;
            _waitTimer = _data.immediately ? 0.0f : _system.GenerateExecuteCommandPause(_data.systemSkillLevel, _data.executeCommandPauseCoeff);

            Debug.LogError($"Execute() {data.Type.ToString()} / pause {_waitTimer}");

            SetState(EnumSpaceshipSystemActionStates.Waiting);
        }


        public void TryUpdateTargetNormalizeSpeedMod(float newTargetNormalizeSpeedMod, bool immediately)
        {
            if (State == EnumSpaceshipSystemActionStates.Waiting || State == EnumSpaceshipSystemActionStates.Started)
            {
                if (immediately)
                    _waitTimer = 0.0f;

                _data.targetNormalizeSpeedMod = newTargetNormalizeSpeedMod;
            }
        }
    }
}
