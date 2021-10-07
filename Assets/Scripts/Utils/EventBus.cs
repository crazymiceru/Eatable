using System;
using System.Collections.Generic;

public static class EventBus
{
    //int
    private static EventBus<int> EB_int = new EventBus<int>();
    public static void Subscribe(string variable, Action<int> action) => EB_int.Subscribe(variable, action);
    public static void UnSubscribe(string variable, Action<int> action) => EB_int.UnSubscribe(variable, action);
    public static void RaiseEvent(string variable, int value) => EB_int.RaiseEvent(variable, value);

    //float
    private static EventBus<float> EB_float = new EventBus<float>();
    public static void Subscribe(string variable, Action<float> action) => EB_float.Subscribe(variable, action);
    public static void UnSubscribe(string variable, Action<float> action) => EB_float.UnSubscribe(variable, action);
    public static void RaiseEvent(string variable, float value) => EB_float.RaiseEvent(variable, value);
}

public class EventBus<T>
{
    private Dictionary<string, List<Action<T>>> _subscribers = new Dictionary<string, List<Action<T>>>();

    public void Subscribe(string variable, Action<T> action)
    {
        if (!_subscribers.ContainsKey(variable)) _subscribers[variable] = new List<Action<T>>();
        _subscribers[variable].Add(action);
    }
    public void UnSubscribe(string variable, Action<T> action)
    {
        if (!_subscribers.ContainsKey(variable)) return;
        _subscribers[variable].Remove(action);
    }
    public void RaiseEvent(string variable, T value)
    {
        if (!_subscribers.ContainsKey(variable)) return;
        foreach (var item in _subscribers[variable])
        {
            item?.Invoke(value);
        }
    }
}
