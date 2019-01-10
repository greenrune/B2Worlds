using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriger : MonoBehaviour {

	public GameObject [] obstacles;
	// Use this for initialization
	void Start () {
		GameObject clone = Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform.position, transform.rotation) as GameObject;
		clone.transform.SetParent(gameObject.transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
}
