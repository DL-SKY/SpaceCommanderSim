using SCS.Enums;
using UnityEngine;

namespace SCS.Spaceships.Systems.Actions.Navigation
{
    public class ActionMoveToData : ActionData
    {
        public override EnumSpaceshipSystemActions Type { get { return EnumSpaceshipSystemActions.MoveTo;} }

        public Transform target;
    }


    public class ActionMoveTo : Actions.Action
    {
        private ActionMoveToData _data;


        public ActionMoveTo(Spaceship spaceship) : base(spaceship)
        {
            //reserved
        }


        public override void DoUpdate(float deltaTime)
        {
            if (State == EnumSpaceshipSystemActionStates.Started)
            { 
            
            }
        }

        public override void Execute(ActionData data)
        {
            SetState(EnumSpaceshipSystemActionStates.Started);

            _data = (ActionMoveToData)data;
        }
    }
}
