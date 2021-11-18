using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Constants;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.Services;
using DllSky.StarterKITv2.UI.Windows.MainMenuExample;
using System;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.Windows.Loading
{
    public class GameLoadingWindow : WindowBase, IWindowSceneLoader
    {
        public const string prefabPath = @"PrefabsStarterKIT\UI\Windows\Loading\GameLoadingWindow";

        public event Action OnSceneLoaded;

        public string SceneName { get; private set; }
        public AsyncOperation LoadingProc { get; private set; }

        private ScenesHelper _scenesHelper;
        private IWindowInitializer _window;


        public override void Initialize(object data)
        {
            SceneName = ConstantScenes.EXAMPLE_MAIN_MENU;

            _scenesHelper = ComponentLocator.Resolve<ScenesHelper>();
            LoadingProc = _scenesHelper.LoadSceneAsync(SceneName);
            LoadingProc.completed += OnLoadSceneCompletedHandler;

            SetInitialize(true);
        }

        public void LoadScene(string scene) { }

        public void CloseLoaderWindow()
        {
            Close();
        }


        private void OnLoadSceneCompletedHandler(AsyncOperation operation)
        {
            if (LoadingProc != null)
                LoadingProc.completed -= OnLoadSceneCompletedHandler;

            OnSceneLoaded?.Invoke();

            var windowData = UnityEngine.Random.Range(1, 1001);         //Пример того, что окну можно передавать какие-нибудь данные, н-р, необходимые для отображения
            _window = GameManager.Instance.WindowsController.CreateWindow<MainMenuExample.MainMenuWindow>(MainMenuExample.MainMenuWindow.prefabPath, Enums.EnumWindowsLayer.Main, data: windowData);

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
