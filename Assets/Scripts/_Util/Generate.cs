using UnityEngine;
using System.Collections;

public class Generate : MonoBehaviour {

    public GameObject rocks;
    public GameObject Floor;
    public float HongosTimeRate = 0;
    public float FloorTimeRate = 0;
    
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("CreateObstacle", 1f, HongosTimeRate);
        InvokeRepeating("CreateFloorObstacle", 1f, FloorTimeRate);
    }

   
    void CreateObstacle()
    {
        Instantiate(rocks, transform.position, transform.rotation);        
    }

    void CreateFloorObstacle()
    {
        Instantiate(Floor, transform.position, transform.rotation);       
    }
}
