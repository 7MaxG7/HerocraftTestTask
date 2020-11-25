using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TEventInvoker<T> : MonoBehaviour {
    protected Dictionary<EventNameEnum, UnityEvent<T>> events;
    protected virtual void Awake() {
        events = new Dictionary<EventNameEnum, UnityEvent<T>>();
    }

    public void AddListener(EventNameEnum event_foo, UnityAction<T> listener_foo) {
        if (events.ContainsKey(event_foo)) {
            events[event_foo].AddListener(listener_foo);
        }
    }
}
