using SCS.Datas.Spaceships;
using SCS.Spaceships;
using UnityEngine;

namespace SCS.Scenes.TestV0
{
    [RequireComponent(typeof(Spaceship))]
    public class SpaceshipAddToSpaceManager : MonoBehaviour
    {
        [SerializeField] private bool _isMineSpaceship;


        private void Start()
        {
            var spaceship = GetComponent<Spaceship>();
            spaceship.Initialize(new SpaceshipData() { isMine = _isMineSpaceship });
        }
    }
}
