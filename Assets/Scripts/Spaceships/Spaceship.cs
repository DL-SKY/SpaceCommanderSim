using SCS.Datas.Spaceships;
using SCS.GameModes.Space.Managers;
using SCS.Interfaces.SpaceObjects;
using SCS.Interfaces.Spaceships.Parameters;
using SCS.Interfaces.Spaceships.Systems;
using SCS.Interfaces.Transforms;
using SCS.ScriptableObjects.Spaceships;
using SCS.Spaceships.Parameters;
using SCS.Spaceships.Systems;
using UnityEngine;

namespace SCS.Spaceships
{
    public class Spaceship : MonoBehaviour, ITransformCache, IParametersContainerInclude, ISystemsControllerInclude, IMinDistanceRadius
    {
        public Transform TransformSelf { get; private set; }
        public SpaceshipParametersContainer Parameters { get; private set; }
        public SpaceshipSystemsController Systems => _systems;
        public float MinDistanceRadius => _config?.minDistanceRadius ?? 0.0f;
        public bool IsMine => _data?.isMine ?? false;


        [Header("Base config")]
        [SerializeField] private SpaceshipConfig _config;

        [Header("Systems")]
        [SerializeField] private SpaceshipSystemsController _systems;

        [Header("Links")]
        [SerializeField] private Transform _targetUI;
        [SerializeField] private Rigidbody _rigidbody;

        private SpaceshipData _data;


        private void Awake()
        {
            TransformSelf = transform;
            Parameters = new SpaceshipParametersContainer();
        }


        public void Initialize(SpaceshipData data)
        {
            _data = data;

            var spaceManager = DllSky.StarterKITv2.Services.ComponentLocator.Resolve<SpaceManager>();
            if (spaceManager)
                spaceManager.AddSpaceship(this);

            Parameters.Initialize(_data);
            _systems.Initialize(this);
        }

        public SpaceshipData GetData()
        {
            return _data;
        }

        public Transform GetTargetUI()
        {
            return _targetUI;
        }

        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }
    }
}
