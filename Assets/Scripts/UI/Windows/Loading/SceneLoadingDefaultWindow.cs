using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.Services;
using DllSky.StarterKITv2.UI.Windows;
using System;
using UnityEngine;

namespace SCS.UI.Windows.Loading
{
    public class SceneLoadingDefaultWindow : WindowBase, IWindowSceneLoader
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Loading\SceneLoadingDefaultWindow";

        public event Action OnSceneLoaded;

        public string SceneName { get; private set; }
        public AsyncOperation LoadingProc { get; private set; }

        private ScenesHelper _scenesHelper;


        private void Awake()
        {
            _scenesHelper = ComponentLocator.Resolve<ScenesHelper>();
        }


        public override void Initialize(object data)
        {
            SceneName = (string)data;

            LoadingProc = _scenesHelper.LoadSceneAsync(SceneName);
            LoadingProc.completed += OnLoadSceneCompletedHandler;

            SetInitialize(true);
        }

        public void LoadScene(string scene) 
        { 
            //Reserved
        }

        public void CloseLoaderWindow()
        {
            Close();
        }


        private void OnLoadSceneCompletedHandler(AsyncOperation operation)
        {
            if (LoadingProc != null)
                LoadingProc.completed -= OnLoadSceneCompletedHandler;

            OnSceneLoaded?.Invoke();
        }
    }
}
