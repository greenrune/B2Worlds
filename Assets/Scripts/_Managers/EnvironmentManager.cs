using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour {

	public GameObject FogForward;
	public GameObject FogBackward;
	public GameObject fogYellow;
	public GameObject CloudBlue;
	public GameObject Cloudgreen;

	// Use this for initialization
	public void PauseGame (int value) {
		GameManager.manager.TimeScaleGame(value);
	}
	
	// Update is called once per frame
	public void UpdateFogForward (bool active) {
		FogForward.SetActive(active);
	}
	public void UpdateFogBackward (bool active) {
		FogBackward.SetActive(active);
	}
	public void UpdatefogYellow (bool active) {
		fogYellow.SetActive(active);
	}
	public void UpdateCloudBlue (bool active) {
		CloudBlue.SetActive(active);
	}
	public void UpdateCloudgreen (bool active) {
		Cloudgreen.SetActive(active);
	}


}
