using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floor : MonoBehaviour
{
    
    public int destroyDistance = -55;
    public bool isEffect;
    public Vector2 testSpeed;

    [HeaderAttribute("Test Mode")]
    public bool testMode;

    // Use this for initialization
    void Start()
    {
        if(!testMode) GetComponent<Rigidbody2D>().velocity = GameManager.manager.platformVelicity;
        else if(testMode) GetComponent<Rigidbody2D>().velocity = testSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.x < destroyDistance && !isEffect)
        {
            PlatformManager.destructor.GeneratePlatform();
            Destroy(this.gameObject);
        }
        else if (transform.position.x < destroyDistance && isEffect)
        {
            Destroy(this.gameObject);
        }

        if(GameManager.manager.gameOver) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        else if(GameManager.manager.startGame) GetComponent<Rigidbody2D>().velocity = GameManager.manager.platformVelicity;
    }
}
