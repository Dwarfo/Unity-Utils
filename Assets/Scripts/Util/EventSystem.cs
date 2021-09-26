using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSystem : Singleton_MB<EventSystem>
{
    /*all events mapped by name*/
    private Dictionary<string, UnityEvent<UEventArgs>> eventMap = new Dictionary<string, UnityEvent<UEventArgs>>();
    
    /*Generic start method. Adds input events by default*/
    private void Start()
    {
        AddEvent("input_leftMouse", new UnityEvent<UEventArgs>());
        AddEvent("input_rightMouse", new UnityEvent<UEventArgs>());
    }

    public void AddEvent(string eventName, UnityEvent<UEventArgs> uEvent) 
    {
        if (!eventMap.ContainsKey(eventName))
        {
            eventMap.Add(eventName, uEvent);
        }
    }

    public void StartListening(string eventName, string sender, bool notify, UnityAction<UEventArgs> listener)
    {
        UnityEvent<UEventArgs> thisEvent = null;
        if (eventMap.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent<UEventArgs>();
            thisEvent.AddListener(listener);
            eventMap.Add(eventName, thisEvent);
        }

        //if(notify)
            //UIManager.Instance.logger.LogListener(sender, eventName);
    }

    public void StopListening(string eventName, UnityAction<UEventArgs> listener)
    {
        UnityEvent<UEventArgs> thisEvent = null;
        if (eventMap.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public void FireEvent(string eventName, UEventArgs args)
    {
        UnityEvent<UEventArgs> thisEvent = null;
        if (eventMap.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(args);
        }

        //if(args.toLog)
            //UIManager.Instance.logger.LogEvent(args.argSource, eventName);
    }

    
}

public class UEventArgs 
{
    public UEventArgs(string argSource, bool toLog)
    {
        this.argSource = argSource;
        this.toLog = toLog;
    }
    public string argName;
    public string argSource;
    public bool toLog;

}

public class InputArgs : UEventArgs 
{
    public InputArgs(string argSource, bool toLog) : base(argSource, toLog)
    {

    }
}