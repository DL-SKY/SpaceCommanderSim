using SCS.Datas.Spaceships.Systems;
using SCS.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Spaceships.Systems
{
    public class SpaceshipSystemsController : MonoBehaviour
    {
        [SerializeField] private SpaceshipSystemDictionaryData[] _systemsArray;
        private Dictionary<EnumSpaceshipSystems, SpaceshipSystem> _systems;

        private bool _isInit;
        private Spaceship _spaceship;


        private void Awake()
        {
            CreateSystemsDictionary(_systemsArray);
        }

        private void Update()
        {
            if (!_isInit)
                return;

            foreach (var system in _systems)
                system.Value.DoUpdate(Time.deltaTime);
        }


        public void Initialize(Spaceship spaceship)
        {
            _spaceship = spaceship;
            _isInit = true;

            foreach (var system in _systems)
                system.Value.Initialize(_spaceship);
        }


        private void CreateSystemsDictionary(SpaceshipSystemDictionaryData[] array)
        {
            _systems = new Dictionary<EnumSpaceshipSystems, SpaceshipSystem>();

            if (array == null)
                return;

            foreach (var data in array)
                if (!_systems.ContainsKey(data.type))
                    _systems.Add(data.type, data.link);
        }
    }
}
