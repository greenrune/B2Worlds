using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public enum Axis { AxisX, AxisY, AxisZ}
    public Axis Pivot;
    public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        switch (Pivot)
        {
            case Axis.AxisX:
                transform.Rotate(Vector3.left * speed );
                break;
            case Axis.AxisY:
                transform.Rotate(Vector3.up * speed);
                break;
            case Axis.AxisZ:
                transform.Rotate(Vector3.forward * speed );
                break;
            default:
                break;
        }
    }
}
