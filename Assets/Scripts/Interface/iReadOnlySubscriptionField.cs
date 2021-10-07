using System;

public interface IReadOnlySubscriptionField<T>
{
    T Value { get; }
    void Subscribe(Action<T> func);
    void UnSubscribe(Action<T> func);
}
