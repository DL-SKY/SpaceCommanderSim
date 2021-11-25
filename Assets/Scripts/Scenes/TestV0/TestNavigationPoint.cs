using DllSky.StarterKITv2.Services;
using SCS.GameModes.Space.Managers;
using SCS.Spaceships.Systems.Actions.Navigation;
using UnityEngine;

namespace SCS.Scenes.TestV0
{
    public class TestNavigationPoint : MonoBehaviour
    {
        [ContextMenu("Set This Navigation Point")]
        private void SetNavigationPoint()
        {
            var data = new ActionMoveToData
            {
                systemSkillLevel = 1,
                executeCommandPauseCoeff = 1.0f,
                target = transform,
                targetMinDistanceRadius = 1.5f,
            };

            ComponentLocator.Resolve<SpaceManager>()?.
                MineSpaceship?.Systems?.
                GetSystem(Enums.EnumSpaceshipSystems.Navigation)?.
                DoAction(data);
        }
    }
}
