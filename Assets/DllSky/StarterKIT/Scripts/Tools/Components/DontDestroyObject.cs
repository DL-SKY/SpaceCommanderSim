using UnityEngine;

namespace DllSky.StarterKITv2.Tools.Components
{
    public class DontDestroyObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
