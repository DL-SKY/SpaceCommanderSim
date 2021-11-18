using System;
using UnityEngine;

namespace DllSky.StarterKITv2.Interfaces.Pool
{
    public interface IPoolObject
    {
        Action<GameObject> OnReturnObject { get; set; }

        bool IsUsing { get; }
        GameObject PoolObject{ get; }

        void ShowObject();
        void ReturnObject();
    }
}
