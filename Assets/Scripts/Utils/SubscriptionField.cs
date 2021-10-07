using System;

public sealed class SubscriptionField<T> : IReadOnlySubscriptionField<T>
{
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            evtChange.Invoke(value);
        }
    }

    private T _value;
    private event Action<T> evtChange = delegate { };

    public void Subscribe(Action<T> func)
    {
        evtChange += func;
    }

    public void UnSubscribe(Action<T> func)
    {
        evtChange -= func;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
