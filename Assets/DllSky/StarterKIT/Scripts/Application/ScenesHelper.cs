using DllSky.StarterKITv2.Tools.Components;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DllSky.StarterKITv2.Application
{
    public class ScenesHelper : AutoLocatorObject
    {
        public event Action<string> OnSceneLoaded;


        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoadedHandler;
        }

        protected override void CustomOnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoadedHandler;
        }


        public AsyncOperation LoadSceneAsync(string sceneName)
        {
            return SceneManager.LoadSceneAsync(sceneName);
        }

        public bool CheckCurrentScene(string sceneName)
        {
            return SceneManager.GetActiveScene().name == sceneName;
        }


        private void OnSceneLoadedHandler(Scene scene, LoadSceneMode loadSceneMode)
        {
            Debug.Log("[ScenesHelper] OnSceneLoadedHandler() " + scene.name);
            OnSceneLoaded?.Invoke(scene.name);
        }
    }
}
