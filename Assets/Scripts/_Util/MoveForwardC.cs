using UnityEngine;
using System.Collections;

public class MoveForwardC : MonoBehaviour
{

    public float Speed = 20;
    public Vector3 relativeDirection = Vector3.forward;
    public float duration = 1.0f;

    public bool destroy = true;
    public bool isUI = false;
    private RectTransform Rect;
    void Start()
    {
        Rect = GetComponent<RectTransform>();
        if (destroy) Destroy(gameObject, duration);
    }

    void Update()
    {

        if (!isUI)
        {
            Vector3 absoluteDirection = transform.rotation * relativeDirection;
            transform.position += absoluteDirection * Speed * Time.deltaTime;
        }
        else if (isUI)
        {
            Rect.anchoredPosition = new Vector2(Rect.anchoredPosition.x, Rect.anchoredPosition.y + 1 * Speed * Time.deltaTime); ;
            // GetComponent<RectTransform>().position = new Vector2(absoluteDirection.x, absoluteDirection.y * Speed * Time.deltaTime) ;
        }

    }
}
