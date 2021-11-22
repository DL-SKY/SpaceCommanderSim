using SCS.Enums;
using SCS.Interfaces.ECS;

namespace SCS.Spaceships.Systems.Actions
{
    public abstract class ActionData
    { 
        public abstract EnumSpaceshipSystemActions Type { get; }
    }


    public abstract class Action : IUpdateSystem
    {
        public EnumSpaceshipSystemActionStates State { get; protected set; }

        protected Spaceship _spaceship;


        public Action(Spaceship spaceship)
        {
            _spaceship = spaceship;
        }


        protected void SetState(EnumSpaceshipSystemActionStates state)
        {
            State = state;
        }


        public abstract void DoUpdate(float deltaTime);
        public abstract void Execute(ActionData data);
    }
}
