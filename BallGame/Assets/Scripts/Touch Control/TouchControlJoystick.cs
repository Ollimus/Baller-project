using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchControlJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    int movementRange = 30;
    Vector2 startPos;
    float horizontal = 0f;
    public bool isJoystickActive;

    void Start()
    {
        startPos = transform.position;
    }

    public virtual void OnPointerDown(PointerEventData data)
    {
        isJoystickActive = true;
        OnDrag(data);
    }

    public virtual void OnPointerUp(PointerEventData data)
    {
        isJoystickActive = false;
        transform.position = startPos;
    }

    public virtual void OnDrag(PointerEventData data)
    {
        Vector2 newPos = Vector2.zero;

        float delta = (int)(data.position.x - startPos.x);
        delta = Mathf.Clamp(delta, -movementRange, movementRange);
        newPos.x = delta;

        transform.position = new Vector2(startPos.x + newPos.x, startPos.y);        
    }

    public float HorizontalJoystick()
    {
        horizontal = Mathf.InverseLerp(transform.position.x - startPos.x, -1.0f, 1.0f);

        return horizontal = (transform.position.x - startPos.x) > 0 ? horizontal : -(horizontal);
    }
}