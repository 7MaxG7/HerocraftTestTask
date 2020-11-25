using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TEventManager<T> {
    static Dictionary<EventNameEnum, List<TEventInvoker<T>>> invokers = new Dictionary<EventNameEnum, List<TEventInvoker<T>>>();
    static Dictionary<EventNameEnum, List<UnityAction<T>>> listeners = new Dictionary<EventNameEnum, List<UnityAction<T>>>();

    public static void Initialize() {
        foreach (EventNameEnum eventName in Enum.GetValues(typeof(EventNameEnum))) {
            if (!invokers.ContainsKey(eventName)) {
                invokers.Add(eventName, new List<TEventInvoker<T>>());
                listeners.Add(eventName, new List<UnityAction<T>>());
            } else {
                invokers[eventName].Clear();
                listeners[eventName].Clear();
            }
        }
    }
    public static void AddInvoker(EventNameEnum eventName_foo, TEventInvoker<T> invoker_foo) {
        if (!invokers[eventName_foo].Contains(invoker_foo)) {
            invokers[eventName_foo].Add(invoker_foo);
            foreach (UnityAction<T> listener in listeners[eventName_foo]) {
                invoker_foo.AddListener(eventName_foo, listener);
            }
        }
    }
    public static void AddListener(EventNameEnum eventName_foo, UnityAction<T> listener_foo) {
        if (!listeners[eventName_foo].Contains(listener_foo)) {
            listeners[eventName_foo].Add(listener_foo);
            foreach (TEventInvoker<T> invoker in invokers[eventName_foo]) {
                invoker.AddListener(eventName_foo, listener_foo);
            }
        }
    }
    public static void RemoveInvoker(EventNameEnum eventName_foo, TEventInvoker<T> invoker_foo) {
        invokers[eventName_foo].Remove(invoker_foo);
    }
}
