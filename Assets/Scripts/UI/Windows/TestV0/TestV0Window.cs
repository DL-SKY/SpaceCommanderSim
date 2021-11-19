using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Enums;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.Services;
using DllSky.StarterKITv2.UI.Windows;
using SCS.Application;
using SCS.UI.Windows.Loading;

namespace SCS.UI.Windows.MainMenu
{
    public class TestV0Window : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Test\TestV0Window";

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
            if (_scenesHelper.CheckCurrentScene(Constants.ConstantScenes.TEST_0))
            {
                SetInitialize(true);
            }
            else
            {
                _loadingWindow = _windowsManagerHolder.WindowsController.CreateWindow<SceneLoadingDefaultWindow>
                    (
                        SceneLoadingDefaultWindow.prefabPath,
                        EnumWindowsLayer.Loading,
                        Constants.ConstantScenes.TEST_0
                    );
                _loadingWindow.OnSceneLoaded += OnSceneLoadedHandler;
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


        protected override void CustomOnClickEsc()
        {
            _windowsManagerHolder.WindowsController.CreateWindow<MainMenuWindow>(MainMenuWindow.prefabPath, EnumWindowsLayer.Main);
        }
    }
}
