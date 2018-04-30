using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControlJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    Vector2 startPos;
    float horizontal = 0f;
    int movementRange = 30; //Allowed movement range for joystick.
    public float minimumInput = 0.99f; //Input value from joystick needs to be higher than this to register input. Higher value increases "sensitive" area around 0.
    float flatInputValue = 1f; //Use flat input if you want more precise control.

    public bool isJoystickActive; //PlayerMovementController checks whether mobile joystick is active and giving input.

    void Start()
    {
        startPos = transform.position;
    }

    public virtual void OnPointerDown(PointerEventData data)
    {
        isJoystickActive = true;
    }

    public virtual void OnPointerUp(PointerEventData data)
    {
        transform.position = startPos;
        isJoystickActive = false;
    }

    //Handles moving joystick image accordingly.
    public virtual void OnDrag(PointerEventData data)
    {
        Vector2 newPos = Vector2.zero;

        float delta = (int)(data.position.x - startPos.x);
        delta = Mathf.Clamp(delta, -movementRange, movementRange);  //Clamps the value of delta between negative and positive movementRange
        newPos.x = delta;

        transform.position = new Vector2(startPos.x + newPos.x, startPos.y);        
    }

    /*
     * Handles mobile joystick input to PlayerMovementController
    */
    public float HorizontalJoystick()
    {
        bool isAxisPositive = transform.position.x > startPos.x;

        if (isAxisPositive)
        {
            horizontal = Mathf.InverseLerp(0f, movementRange, (transform.position.x - startPos.x));

            return (horizontal > minimumInput && horizontal > 0) ? flatInputValue : 0;
        }

        else
        {
            horizontal = -(Mathf.InverseLerp(0f, -movementRange, (transform.position.x - startPos.x)));

            return (horizontal < minimumInput && horizontal < 0) ? -flatInputValue : 0;
        }
    }
}

/*
 *     public float HorizontalJoystick()
    {
        bool isAxisPositive = transform.position.x > startPos.x; 

        if (isAxisPositive)
        {
            horizontal = Check(movementRange);
        }

        else
        {
            horizontal = Check(-movementRange);
        }
    }

    private float Check(float range)
    {
        horizontal = Mathf.InverseLerp(0f, range, (transform.position.x - startPos.x));
        
        if (range > 0)
        {
            return (horizontal > minimumInput && horizontal > 0) ? flatInputValue : 0;
        }

        else
        {
            if (horizontal < -minimumInput && horizontal < 0)
            {
                return -flatInputValue;
            }

            else
            {
                return 0;
            }
        }
    }
*/