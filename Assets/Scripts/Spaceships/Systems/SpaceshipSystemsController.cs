using DllSky.StarterKITv2.Services;
using SCS.Application;
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

        private GlobalConfigManager _clobalConfigs;


        private void Awake()
        {
            CreateSystemsDictionary(_systemsArray);

            _clobalConfigs = ComponentLocator.Resolve<GlobalConfigManager>();
        }

        private void Update()
        {
            if (!_isInit)
                return;

            foreach (var system in _systems)
                system.Value?.DoUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (!_isInit)
                return;

            foreach (var system in _systems)
                system.Value?.DoFixedUpdate(Time.fixedDeltaTime);
        }


        public void Initialize(Spaceship spaceship)
        {
            _spaceship = spaceship;
            _isInit = true;

            foreach (var system in _systems)
                system.Value?.Initialize(_spaceship);
        }

        public SpaceshipSystem GetSystem(EnumSpaceshipSystems system)
        {
            if (_systems.ContainsKey(system))
                return _systems[system];
            else
                return null;
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
