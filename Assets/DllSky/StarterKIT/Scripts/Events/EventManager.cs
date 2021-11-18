using System;
using System.Collections.Generic;

namespace DllSky.StarterKITv2.Events
{
    [Obsolete]
    public static class EventManager
    {
        private static Dictionary<string, CustomEventWrapper> _events = new Dictionary<string, CustomEventWrapper>();


        public static void AddEventListener(string eventType, CustomEventHandler listener)
        {
            CustomEventWrapper eWrapper = null;

            if (_events.TryGetValue(eventType, out eWrapper))
            {
                eWrapper.OnHandler += listener;                
            }
            else
            {
                eWrapper = new CustomEventWrapper();
                eWrapper.OnHandler += listener;
                _events.Add(eventType, eWrapper);
            }
        }

        public static void RemoveEventListener(string eventType, CustomEventHandler listener)
        {
            CustomEventWrapper eWrapper = null;

            if (_events.TryGetValue(eventType, out eWrapper))
            {
                eWrapper.OnHandler -= listener;
            }
        }

        public static void DispatchEvent(CustomEvent e)
        {
            CustomEventWrapper eWrapper = null;

            if (_events.TryGetValue(e.EventType, out eWrapper))
            {
                eWrapper.Invoke(e);
            }
        }

        public static void DispatchEvent(string type, object data = null)
        {
            var customEvent = new CustomEvent(type, data);
            DispatchEvent(customEvent);
        }

        public static void Clear()
        {
            foreach (var e in _events)
                e.Value.Clear();

            _events.Clear();
        }
    }
}