using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateUI : MonoBehaviour {

    public bool activateGUIAnim;	
	
	// Use this for initialization
	void Start () {
		//GameManager.manager.canvasUI.transform.GetChild(0).gameObject.SetActive(true);
        if (activateGUIAnim) GameManager.manager.animMaster.StartAnim ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
