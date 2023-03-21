using System;

namespace Utils
{
    [Serializable]
    public class EventVar<T>
    {
        // Declare the delegate (if using non-generic pattern).
        public delegate void OnChangeEventHandler(T value);

        // Declare the event.
        public event OnChangeEventHandler OnChangeEvent;

        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;

                OnChangeEvent?.Invoke(Value);
            }
        }

        public EventVar(T value)
        {
            Value = value;
        }

        public void Set(T value)
        {
            Value = value;
        }

        public void Set(Func<T, T> func)
        {
            Value = func(Value);
        }
    }
}