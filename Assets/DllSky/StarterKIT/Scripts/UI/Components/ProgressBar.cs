using UnityEngine;
using UnityEngine.UI;

namespace DllSky.StarterKITv2.UI.Components
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] protected Image _mainProgressBar;

        public float FillAmount
        {
            set { _mainProgressBar.fillAmount = value; }
            get { return _mainProgressBar.fillAmount; }
        }
    }
}
