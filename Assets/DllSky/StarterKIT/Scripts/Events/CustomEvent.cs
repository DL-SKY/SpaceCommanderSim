using System;

namespace DllSky.StarterKITv2.Events
{
    [Obsolete]
    public delegate void CustomEventHandler(CustomEvent _event);

    [Obsolete]
    public class CustomEvent
    {
        public string EventType
        {
            get;
            private set;
        }

        public object EventData
        {
            get;
            private set;
        }

        public CustomEvent(string type, object data = null)
        {
            EventType = type;
            EventData = data;
        }
    }
}