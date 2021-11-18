using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.Services;
using DllSky.StarterKITv2.UI.Windows;
using SCS.Application;
using SCS.UI.Windows.MainMenu;
using System;
using UnityEngine;

namespace SCS.UI.Windows.Loading
{
    public class GameLoadingWindow : WindowBase, IWindowSceneLoader
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Loading\GameLoadingWindow";

        public event Action OnSceneLoaded;

        public string SceneName { get; private set; } = Constants.ConstantScenes.MAIN_MENU_0;
        public AsyncOperation LoadingProc { get; private set; }

        private ScenesHelper _scenesHelper;
        private IWindowInitializer _window;


        public override void Initialize(object data)
        {
            _scenesHelper = ComponentLocator.Resolve<ScenesHelper>();
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

            _window = SCSGameManager.Instance.WindowsController.CreateWindow<MainMenuWindow>(MainMenuWindow.prefabPath, DllSky.StarterKITv2.Enums.EnumWindowsLayer.Main);

            if (_window.IsInit)
                OnInitializeHandler();
            else
                _window.OnInitialize += OnInitializeHandler;
        }


        private void OnInitializeHandler()
        {
            _window.OnInitialize -= OnInitializeHandler;
            Close();
        }
    }
}
