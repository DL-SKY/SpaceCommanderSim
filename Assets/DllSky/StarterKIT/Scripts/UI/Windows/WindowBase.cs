using DllSky.StarterKITv2.Interfaces.Windows;
using System;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour, IWindowInitializer
    {
        public event Action OnInitialize;
        public event Action<bool, WindowBase> OnClose;  
        
        public bool IsInit { get; private set; }

        [Header("Main Settings")]
        [SerializeField] protected bool _canUseEsc;


        public virtual void Initialize(object data)
        {            
            SetInitialize(true);
        }

        public void Close(bool result = false)
        {
            OnClose?.Invoke(result, this);
            Destroy(gameObject);

            CustomClose(result);
        }

        public void OnClickEsc()
        {
            if (_canUseEsc)
                Close();

            CustomOnClickEsc();
        }


        protected void SetInitialize(bool state)
        {
            IsInit = state;
                
            if (IsInit)
                OnInitialize?.Invoke();
        }


        protected virtual void CustomClose(bool result) { }
        protected virtual void CustomOnClickEsc() { }
    }
}
