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
            spaceship.Initialize(new SpaceshipData() 
            { 
                isMine = _isMineSpaceship,
                parameters = new System.Collections.Generic.List<SpaceshipParameterRangeData>
                { 
                    //Main,

                    //Shields,

                    //Navigation,
                    new SpaceshipParameterRangeData { parameter = Enums.EnumSpaceshipParameters.Speed, range = new Vector2(10.0f, 12.5f) },
                    new SpaceshipParameterRangeData { parameter = Enums.EnumSpaceshipParameters.Maneuver, range = new Vector2(100.0f, 180.0f) },
                    new SpaceshipParameterRangeData { parameter = Enums.EnumSpaceshipParameters.AccelerateTime, range = new Vector2(5.0f, 5.0f) },

                    new SpaceshipParameterRangeData { parameter = Enums.EnumSpaceshipParameters.RotationZ, range = new Vector2(0.0f, 0.0f) },                    

                    //Weapons,

                    //Energy,

                    //Radar,

                    //Oxygen,
                }
            });
        }
    }
}
