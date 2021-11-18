using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Services;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.WindowsManager
{
    public class WorldCanvasCameraChanger : MonoBehaviour
    {
        [SerializeField] private Canvas _worldCanvas;
        private ScenesHelper _scenesHelper;


        private void OnEnable()
        {
            _scenesHelper = ComponentLocator.Resolve<ScenesHelper>();
            _scenesHelper.OnSceneLoaded += OnSceneLoadedHandler;
        }

        private void OnDisable()
        {
            _scenesHelper.OnSceneLoaded -= OnSceneLoadedHandler;
        }


        private void OnSceneLoadedHandler(string sceneName)
        {
            _worldCanvas.worldCamera = Camera.main;
        }
    }
}
