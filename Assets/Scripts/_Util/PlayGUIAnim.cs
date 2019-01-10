using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGUIAnim : MonoBehaviour {
    public GEAnim [] startAnimSprite;

    public GEAnim enhancerPanel;
    
    // Use this for initialization
    public void StartAnim () {
        
        // for (int i = 0; i < startAnimSprite.Length; i++)
        // {
        //     Image[] allImage = startAnimSprite[i].gameObject.transform.GetComponentsInChildren<Image>();
        //     Text[] allText = startAnimSprite[i].gameObject.transform.GetComponentsInChildren<Text>();

        //     for (int j = 0; j < allText.Length; i++)
        //     {
        //         allText[j].color = new Color(255, 255, 255, 0);
        //     }
        //     for (int k = 0; k < allImage.Length; i++)
        //     {
        //         allImage[k].color = new Color(255, 255, 255, 0);
        //     }            
        // }

        for (int i = 0; i < startAnimSprite.Length; i++)
        {
            if(startAnimSprite[i].gameObject.activeInHierarchy) startAnimSprite[i].MoveIn();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
