using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHadler : MonoBehaviour {

	
    void Update()
    {
        if (Input.anyKeyDown)
        {
            EventManager.listener.m_MyEvent.Invoke();
        }
    }
	
}
