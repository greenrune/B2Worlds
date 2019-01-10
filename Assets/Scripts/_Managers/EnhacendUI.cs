using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhacendUI : MonoBehaviour {

	public Text enhancedUI;
	public int bdPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		enhancedUI.text = GameManager.manager.shop.itemList[bdPos].itemCount.ToString();
	}
}
