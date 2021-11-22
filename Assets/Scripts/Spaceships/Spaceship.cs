using SCS.ScriptableObjects.Cameras;
using SCS.Spaceships.Systems;
using UnityEngine;

namespace SCS.Spaceships
{
    public class Spaceship : MonoBehaviour
    {
        public Transform TransformSelf { get; private set; }

        [Header("Base config")]
        [SerializeField] private SpaceshipConfig _config;

        [Header("Systems")]
        [SerializeField] private SpaceshipSystemsController _systems;

        [Header("Links")]
        [SerializeField] private Transform _targetUI;
        [SerializeField] private Rigidbody _rigidbody;


        private void Awake()
        {
            TransformSelf = transform;
        }



    }
}
