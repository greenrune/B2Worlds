using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour {

	public GameObject bullet;
	public Transform attackPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			GetComponent<Animator>().CrossFade("shoot", 0.25F);
			Instantiate(bullet, attackPoint.position, attackPoint.rotation);
		}
	}
}
