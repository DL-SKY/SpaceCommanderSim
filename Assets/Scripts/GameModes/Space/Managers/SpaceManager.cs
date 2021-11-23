using DllSky.StarterKITv2.Tools.Components;
using SCS.Spaceships;
using System;
using System.Collections.Generic;

namespace SCS.GameModes.Space.Managers
{
    public class SpaceManager : AutoLocatorObject
    {
        public event Action<Spaceship> OnCreateMineSpaceship;
        public event Action<Spaceship> OnDestroyMineSpaceship;

        public event Action<Spaceship> OnCreateSpaceship;
        public event Action<Spaceship> OnDestroySpaceship;

        public Spaceship MineSpaceship { get; private set; }
        public List<Spaceship> AllSpaceships { get; private set; } = new List<Spaceship>();


        public void AddSpaceship(Spaceship spaceship)
        {
            AllSpaceships.Add(spaceship);
            OnCreateSpaceship?.Invoke(spaceship);

            if (spaceship.IsMine)
            {
                MineSpaceship = spaceship;
                OnCreateMineSpaceship?.Invoke(spaceship);
            }
        }

        public void RemoveSpaceship(Spaceship spaceship)
        {
            AllSpaceships.Remove(spaceship);
            OnDestroySpaceship?.Invoke(spaceship);

            if (spaceship.IsMine)
            {
                MineSpaceship = null;
                OnDestroyMineSpaceship?.Invoke(spaceship);
            }
        }
    }
}
