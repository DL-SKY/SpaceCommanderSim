using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Constants;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.Services;
using DllSky.StarterKITv2.UI.Windows.DialogExample;
using DllSky.StarterKITv2.UI.Windows.Loading;
using DllSky.StarterKITv2.UI.Windows.MainMenuExample;
using DllSky.StarterKITv2.UI.Windows.WindowsQueue;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.Windows.SecondExampleWindow
{
    public class SecondWindow : WindowBase
    {
        public const string prefabPath = @"PrefabsStarterKIT\UI\Windows\Second\SecondWindow";
        
        private GameManager _gameManager;
        private IWindowsManagerUsing _windowsManagerHolder;
        private ScenesHelper _scenesHelper;
        private IWindowSceneLoader _loadingWindow;
        private WindowBase _dialog;


        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _windowsManagerHolder = GameManager.Instance;
            _scenesHelper = ComponentLocator.Resolve<ScenesHelper>();
        }


        public override void Initialize(object data)
        {
            if (_scenesHelper.CheckCurrentScene(ConstantScenes.EXAMPLE_SECOND_SCENE))
            {
                SetInitialize(true);
            }
            else
            {
                _loadingWindow = _windowsManagerHolder.WindowsController.CreateWindow<SceneLoadingWindow>(SceneLoadingWindow.prefabPath, Enums.EnumWindowsLayer.Loading, ConstantScenes.EXAMPLE_SECOND_SCENE);
                _loadingWindow.OnSceneLoaded += OnSceneLoadedHandler;
            }
        }


        public void OnClickShowDialogButton()
        {
            _dialog = _windowsManagerHolder.WindowsController.CreateWindow<ExampleDialogWindow>(ExampleDialogWindow.prefabPath, Enums.EnumWindowsLayer.Dialogs);
            _dialog.OnClose += OnDialogCloseHandler;
        }

        public void OnClickWindowsQueue()
        {
            for (int i = 0; i < 5; i++)
            {
                var priority = Random.Range(1, 999);
                System.Action action = () => { _windowsManagerHolder.WindowsController.CreateWindow<ExampleDialogWindow>(ExampleDialogWindow.prefabPath, Enums.EnumWindowsLayer.Dialogs); };
                //System.Action action = () => { Debug.LogError("TEST Action"); _windowsManagerUser.WindowsController.OnCloseWindow?.Invoke(false, null); };
                var data = new WindowsQueueData(priority, action);
                _gameManager.WindowsQueue.AddToQueue(data);
            }            
        }


        private void OnSceneLoadedHandler()
        {
            if (_loadingWindow != null)
            {
                _loadingWindow.OnSceneLoaded -= OnSceneLoadedHandler;
                _loadingWindow.CloseLoaderWindow();
            }

            SetInitialize(true);
        }

        private void OnDialogCloseHandler(bool result, WindowBase dialog)
        {
            if (_dialog)
                _dialog.OnClose += OnDialogCloseHandler;

            Debug.Log("Диалог [" +  dialog.GetType().ToString() + "] закрылся с результатом : " + result.ToString());
        }


        protected override void CustomClose(bool result)
        {
            var windowData = Random.Range(1, 1001);         //Пример того, что окну можно передавать какие-нибудь данные, н-р, необходимые для отображения
            _windowsManagerHolder.WindowsController.CreateWindow<MainMenuExample.MainMenuWindow>(MainMenuExample.MainMenuWindow.prefabPath, Enums.EnumWindowsLayer.Main, data: windowData);
        }        
    }
}
