using SCS.Datas.Spaceships;
using SCS.GameModes.Space.Managers;
using SCS.ScriptableObjects.Spaceships;
using SCS.Spaceships.Systems;
using UnityEngine;

namespace SCS.Spaceships
{
    public class Spaceship : MonoBehaviour
    {
        public Transform TransformSelf { get; private set; }
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
        }


        public void Initialize(SpaceshipData data)
        {
            _data = data;

            var spaceManager = DllSky.StarterKITv2.Services.ComponentLocator.Resolve<SpaceManager>();
            if (spaceManager)
                spaceManager.AddSpaceship(this);

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

        public Rigidbody GetRegidbody()
        {
            return _rigidbody;
        }

        public SpaceshipSystemsController GetSystemsController()
        {
            return _systems;
        }
    }
}
