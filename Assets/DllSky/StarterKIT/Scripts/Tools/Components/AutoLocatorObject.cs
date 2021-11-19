using DllSky.StarterKITv2.Services;
using UnityEngine;

namespace DllSky.StarterKITv2.Tools.Components
{
    public class AutoLocatorObject : MonoBehaviour
    {
        [SerializeField] protected bool _isDontDestroy = true;

        [Tooltip("Если NULL - автоопределение")]
        [SerializeField] protected Component _component;


        protected void Awake()
        {
            if (_isDontDestroy)
                TryApplyDontDestroy();            

            if (_component == null)
                _component = this;
            ComponentLocator.Register(_component);

            CustomAwake();
        }

        protected void OnDestroy()
        {
            ComponentLocator.Unregister(_component.GetType());

            CustomOnDestroy();
        }


        protected virtual void CustomAwake() { }
        protected virtual void CustomOnDestroy() { }


        private void TryApplyDontDestroy()
        {
            if (transform.parent == null)
                DontDestroyOnLoad(gameObject);
            else
                Debug.LogWarning($"[AutoLocatorObject] TryApplyDontDestroy() => Object \"{name}\" not root GameObject!");
        }
    }
}
