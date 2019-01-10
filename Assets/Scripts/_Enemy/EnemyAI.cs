using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    //public GameObject body;
    public Vector2 enemySpeed;
    public Vector2 jumpImpulse;
    public bool moving;
	// Use this for initialization
	void Start () {
		 //if(moving) GetComponent<Rigidbody2D>().velocity = enemySpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if(moving) transform.Translate(Vector3.left * enemySpeed.x);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Active");
        }

        if(moving && other.CompareTag("Platform")){
            GetComponent<Rigidbody2D>().AddForce(jumpImpulse);
            print("UP");
        } 
    }
    
}
