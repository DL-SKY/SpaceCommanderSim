using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Enums;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.Services;
using DllSky.StarterKITv2.UI.Windows;
using DllSky.StarterKITv2.UI.Windows.Log;
using SCS.Application;
using SCS.UI.Windows.Loading;

namespace SCS.UI.Windows.MainMenu
{
    public class MainMenuWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\MainMenu\MainMenuV0Window";

        private IWindowsManagerUsing _windowsManagerHolder;
        private ScenesHelper _scenesHelper;
        private IWindowSceneLoader _loadingWindow;


        private void Awake()
        {
            _windowsManagerHolder = SCSGameManager.Instance;
            _scenesHelper = ComponentLocator.Resolve<ScenesHelper>();
        }


        public override void Initialize(object data)
        {
            if (_scenesHelper.CheckCurrentScene(Constants.ConstantScenes.MAIN_MENU_0))
            {
                SetInitialize(true);
            }
            else
            {
                _loadingWindow = _windowsManagerHolder.WindowsController.CreateWindow<SceneLoadingDefaultWindow>
                    (
                        SceneLoadingDefaultWindow.prefabPath,
                        EnumWindowsLayer.Loading,
                        Constants.ConstantScenes.MAIN_MENU_0
                    );
                _loadingWindow.OnSceneLoaded += OnSceneLoadedHandler;
            }
        }

        public void OnClickShowLogWindow()
        {
            _windowsManagerHolder.WindowsController.CreateWindow<LogWindow>(LogWindow.prefabPath, EnumWindowsLayer.Dialogs);
        }

        public void OnClickTest0()
        {
            _windowsManagerHolder.WindowsController.CreateWindow<TestV0Window>(TestV0Window.prefabPath, EnumWindowsLayer.Main);
            Close();
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
    }
}
