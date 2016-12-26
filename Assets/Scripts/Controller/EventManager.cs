using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    //locally cached instance of event manager
    private static EventManager eventManager;

    // public property for getting eventmanager instance
    public static EventManager instance
    {
        get
        {
            if(eventManager==null)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.Log("EventManager: no active event manager script found in scene");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;

        }
    }

    private Dictionary<string, UnityEvent> eventDictionary;

	void Awake()
	{
		ResetStatic ();
	}


    void Init()
    {
        if(eventDictionary==null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }


    // register a listner to listen to an event
    public static void RegisterListner(string eventName, UnityAction listner)
    {
        
        UnityEvent eventToListenTo = null;
        if(instance.eventDictionary.TryGetValue(eventName, out eventToListenTo))
        {
            eventToListenTo.AddListener(listner);
        }
        else
        {
            eventToListenTo = new UnityEvent();
            eventToListenTo.AddListener(listner);
            instance.eventDictionary.Add(eventName, eventToListenTo);
        }
    }

    // remove a listner
    public static void DeRegisterListner(string name, UnityAction listner)
    {
        if (instance == null)
            return;

        UnityEvent eventToStopListenTo = null;
        if(instance.eventDictionary.TryGetValue(name,out eventToStopListenTo))
        {
            eventToStopListenTo.RemoveListener(listner);
        }
    }

    // trigger an event
    public static void TriggerEvent(string name)
    {
        UnityEvent eventToTrigger = null;

        if(instance.eventDictionary.TryGetValue(name,out eventToTrigger))
        {
            eventToTrigger.Invoke();
        }
    }


	private void ResetStatic()
	{
		eventManager = null;
	}


}
