using DllSky.StarterKITv2.Interfaces.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace DllSky.StarterKITv2.Pool
{
    public class LocalPoolManager : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _poolParent;

        [Header("Autoinitializing")]
        [SerializeField] private bool _isAutoInit = false;
        [SerializeField] private uint _defaultPoolSize = 5;

        private List<IPoolObject> _pool = new List<IPoolObject>();


        private void Awake()
        {
            if (_poolParent == null)
                _poolParent = transform;

            if (_isAutoInit)
                Initialize((int)_defaultPoolSize);
        }


        public void Initialize(int poolSize)
        {
            if (poolSize < 1)
                return;

            _pool.Clear();

            for (int i = 0; i < poolSize; i++)
                CreatePoolObject();
        }

        public T GetPoolObject<T>() where T : MonoBehaviour
        {
            var freePoolObject = _pool.Find(x => !x.IsUsing);

            if (freePoolObject == null)
                freePoolObject = CreatePoolObject();

            freePoolObject.ShowObject();

            return freePoolObject.PoolObject.GetComponent<T>();
        }

        public T GetPoolObject<T>(Transform parent) where T : MonoBehaviour
        {
            var freePoolObject = GetPoolObject<T>();
            freePoolObject.transform.SetParent(parent);

            return freePoolObject;
        }


        private IPoolObject CreatePoolObject()
        {
            if (_prefab == null)
            {
                Debug.LogError("[LocalPoolManager " + name + "] : Prefab is NULL!");
                return null;
            }

            var newObject = Instantiate(_prefab, _poolParent);
            newObject.SetActive(false);

            var newPoolObject = newObject.GetComponent<IPoolObject>();
            newPoolObject.OnReturnObject += OnReturnPoolObjectHandler;
            _pool.Add(newPoolObject);

            return newPoolObject;
        }

        private void OnReturnPoolObjectHandler(GameObject poolObject)
        {
            poolObject.transform.SetParent(_poolParent);
            poolObject.SetActive(false);
        }
    }
}
