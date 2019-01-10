using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTexture : MonoBehaviour {

    public float speed;
    public float initTime;
    public bool startMove;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (!GameManager.manager.gameOver)
        {
            initTime = Time.time;
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2((initTime) * (speed), 0);
        }
    }
}
