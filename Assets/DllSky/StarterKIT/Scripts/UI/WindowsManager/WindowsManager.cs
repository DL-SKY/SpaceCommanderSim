using DllSky.StarterKITv2.Enums;
using DllSky.StarterKITv2.Tools.Components;
using DllSky.StarterKITv2.UI.Windows;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace DllSky.StarterKITv2.UI.WindowsManager
{
    [Serializable]
    public class WindowsLayer
    {
        public EnumWindowsLayer key;
        public Transform value;

        public WindowsLayer(EnumWindowsLayer key, Transform value)
        {
            this.key = key;
            this.value = value;
        }
    }

    public class WindowsManager : AutoLocatorObject
    {
        public Action<WindowBase> OnCreateWindow;
        public Action<bool, WindowBase> OnCloseWindow;

        [Space()]
        [SerializeField] private List<WindowsLayer> _layers;

        private List<WindowBase> _windows = new List<WindowBase>();


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                GetLastWindow()?.OnClickEsc();
        }


        public T CreateWindow<T>(string pathPrefab, EnumWindowsLayer windowLayer, object data = null, bool includeInWindowsList = true) where T : WindowBase
        {
            var prefab = Resources.Load<GameObject>(string.Format(pathPrefab));
            var layer = _layers.Find((x) => x.key == windowLayer)?.value ?? transform;
            var window = Instantiate(prefab, layer).GetComponent<T>();
            
            window.OnClose += OnCloseHandler;
            window.Initialize(data);

            if (includeInWindowsList)
                _windows.Add(window);

            OnCreateWindow?.Invoke(window);

            return window;
        }

        public WindowBase GetLastWindow()
        {
            if (_windows.Count < 1)
                return null;

            return _windows[_windows.Count - 1];
        }


        private void OnCloseHandler(bool result, WindowBase window)
        {
            window.OnClose -= OnCloseHandler;

            if (_windows.Contains(window))
                _windows.Remove(window);

            OnCloseWindow?.Invoke(result, window);
        }
    }
}
