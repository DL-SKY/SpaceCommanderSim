using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.Patterns;
using DllSky.StarterKITv2.Services;
using DllSky.StarterKITv2.UI.Windows.FPS;
using DllSky.StarterKITv2.UI.Windows.Loading;
using DllSky.StarterKITv2.UI.Windows.WindowsQueue;
using DllSky.StarterKITv2.UI.WindowsManager;
using System.Collections.Generic;
using UnityEngine;

namespace DllSky.StarterKITv2.Application
{
    public class GameManager : Singleton<GameManager>, IWindowsManagerUsing
    {
        [SerializeField] private bool _isUsingFPS;

        public WindowsManager WindowsController { get; private set; }
        public WindowsQueueController WindowsQueue { get; private set; }


        private void Start()
        {
            WindowsController = ComponentLocator.Resolve<WindowsManager>();
            WindowsQueue = ComponentLocator.Resolve<WindowsQueueController>();

#if UNITY_ANDROID
            UnityEngine.Application.targetFrameRate = 60;
#endif

            Initialize();
        }               


        private void Initialize()
        {
            CreateLoadingWindow();

            WindowsQueue.Reset().SetCheckWindows
                (
                    new List<System.Type> 
                    { 
                        typeof(UI.Windows.SecondExampleWindow.SecondWindow),
                        typeof(UI.Windows.MainMenuExample.MainMenuWindow)
                    }
                );

            if (_isUsingFPS)
                CreateFPSCounter();
        }

        private void CreateLoadingWindow()
        {
            WindowsController.CreateWindow<GameLoadingWindow>(GameLoadingWindow.prefabPath, Enums.EnumWindowsLayer.Loading);
        }

        private void CreateFPSCounter()
        {
            WindowsController.CreateWindow<FPSWindow>(FPSWindow.prefabPath, Enums.EnumWindowsLayer.Special, includeInWindowsList: false);
        }
    }
}
