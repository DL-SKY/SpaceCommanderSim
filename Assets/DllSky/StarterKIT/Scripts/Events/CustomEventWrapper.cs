using System;

namespace DllSky.StarterKITv2.Events
{
    [Obsolete]
    public class CustomEventWrapper
    {
        public event CustomEventHandler OnHandler;

        public void Invoke(CustomEvent e)
        {
            OnHandler?.Invoke(e);
        }

        public void Clear()
        {
            OnHandler = null;
        }
    }
}