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

    PlayerMovementController player;

    void Start()
    {
        startPos = transform.position;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();

        if (player == null)
            Debug.LogError("Player was not found on TC start-up.");
    }

    public PlayerMovementController PlayerMovement
    {
        set
        {
            player = value;
        }
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

        JoyStickMovement();
    }

    public void RefreshPlayerCache(GameObject playerObject)
    {
        player = playerObject.GetComponent<PlayerMovementController>();

        if (player == null)
            Debug.LogError("Touch Control Joystick did not receive Player Object.");
    }

    private void Update()
    {
        if (player != null)
            JoyStickMovement();
    }


    private void JoyStickMovement()
    {
        if (player == null)
            return;

        if (transform.position.x > startPos.x)
        {
            horizontal = Mathf.InverseLerp(0f, movementRange, (transform.position.x - startPos.x));

            player.HorizontalMovement((horizontal > minimumInput && horizontal > 0) ? flatInputValue : 0);
        }

        else
        {
            horizontal = -(Mathf.InverseLerp(0f, -movementRange, (transform.position.x - startPos.x)));

            player.HorizontalMovement((horizontal < -minimumInput && horizontal < 0) ? -flatInputValue : 0);
        }
    }
}