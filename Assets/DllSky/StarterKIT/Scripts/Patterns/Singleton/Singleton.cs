using UnityEngine;

namespace DllSky.StarterKITv2.Patterns
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;


        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }
                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }

                return _instance;
            }
        }

        public static bool IsInstantiated
        {
            get
            {
                return _instance != null;
            }
        }


        protected void Awake()
        {
            if (IsInstantiated == false)
                _instance = GetComponent<T>();

            CustomAwake();
        }

        protected void OnDestroy()
        {
            if (_instance == this)
                _instance = null;

            CustomOnDestroy();
        }

        protected virtual void CustomAwake() { }
        protected virtual void CustomOnDestroy() { }
    }
}
