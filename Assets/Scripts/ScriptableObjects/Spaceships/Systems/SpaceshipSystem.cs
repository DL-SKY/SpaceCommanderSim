using SCS.Interfaces.ECS;
using SCS.Spaceships.Systems.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Spaceships.Systems
{
    public abstract class SpaceshipSystem : MonoBehaviour, IUpdateSystem
    {
        protected Spaceship _spaceship;
        protected Dictionary<Enums.EnumSpaceshipSystemActions, Actions.Action> _actions;


        public void Initialize(Spaceship spaceship)
        {
            _spaceship = spaceship;
            InitializeActionsDictionary();
        }

        
        public abstract void DoUpdate(float deltaTime);
        public abstract void DoAction(ActionData data);

        protected abstract void InitializeActionsDictionary();
    }
}
