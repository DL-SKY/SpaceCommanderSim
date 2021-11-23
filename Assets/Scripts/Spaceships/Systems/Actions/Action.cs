using SCS.Enums;
using SCS.Interfaces.ECS;

namespace SCS.Spaceships.Systems.Actions
{
    public abstract class ActionData
    {
        public int systemSkillLevel = 1;
        public float executeCommandPauseCoeff = 1.0f;

        public abstract EnumSpaceshipSystemActions Type { get; }
    }


    public abstract class Action : IUpdateSystem, IFixedUpdateSystem
    {
        public EnumSpaceshipSystemActionStates State { get; protected set; }

        protected Spaceship _spaceship;
        protected SpaceshipSystem _system;


        public Action(Spaceship spaceship, SpaceshipSystem system)
        {
            _spaceship = spaceship;
            _system = system;
        }


        protected void SetState(EnumSpaceshipSystemActionStates state)
        {
            State = state;
        }        


        public abstract void DoUpdate(float deltaTime);
        public abstract void DoFixedUpdate(float fixedDeltaTime);
        public abstract void Execute(ActionData data);
    }
}
