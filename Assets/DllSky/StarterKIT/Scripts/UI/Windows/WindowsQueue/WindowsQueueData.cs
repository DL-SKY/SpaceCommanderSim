using System;

namespace DllSky.StarterKITv2.UI.Windows.WindowsQueue
{
    public class WindowsQueueData
    {
        private int _priority;
        private Action _action;


        public WindowsQueueData(int priority, Action action)
        {
            _priority = priority;
            _action = action;
        }


        public int GetPriority()
        {
            return _priority;
        }

        public void Execute()
        {
            _action?.Invoke();
        }
    }
}
