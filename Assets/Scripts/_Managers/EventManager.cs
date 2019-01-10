using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

	public static EventManager listener;
	public UnityEvent m_MyEvent;

    void Start()
    {
		if (listener == null)
            listener = this;

        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();

        m_MyEvent.AddListener(Ping);
    }

   
    void Ping()
    {
        Debug.Log("Ping");
    }
}
