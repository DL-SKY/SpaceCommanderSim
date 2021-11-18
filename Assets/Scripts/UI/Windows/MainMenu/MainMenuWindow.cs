using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.Services;
using DllSky.StarterKITv2.UI.Windows;
using SCS.Application;

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
                //TODO
                //_loadingWindow = _windowsManagerHolder.WindowsController.CreateWindow<SceneLoadingWindow>(SceneLoadingWindow.prefabPath, Enums.EnumWindowsLayer.Loading, ConstantScenes.EXAMPLE_MAIN_MENU);
                //_loadingWindow.OnSceneLoaded += OnSceneLoadedHandler;
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
    }
}
