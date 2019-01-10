using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaxScoreShow : MonoBehaviour {
	public TextMeshPro textUI;

	// Use this for initialization
	void Start () {
		textUI.text =  PlayerPrefs.GetInt("Record").ToString() + " m";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
