using DllSky.StarterKITv2.Services;
using UnityEngine;

namespace DllSky.StarterKITv2.Tools.Components
{
    public class AutoLocatorObject : MonoBehaviour
    {
        [Tooltip("Если NULL - автоопределение")]
        [SerializeField] protected Component _component;


        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);

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
    }
}
