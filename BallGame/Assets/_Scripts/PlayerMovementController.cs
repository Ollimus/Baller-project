using UnityEngine;
using System;

public class PlayerMovementController : MonoBehaviour
{

    public float baseMovementSpeed = 6;
    public float rotationalSpeed = 600;
    private float jumpHeight = 8;
    private float horizontalMovementSpeed = 1; //NOTE: DOES NOT USE AXIS INPUT, BECAUSE FLAT VALUE GIVES INCREASED/SMOOTHER MOBILITY WHILE JUMPING.
    private float horizontalInput;

    private GameObject TouchController;
    private TouchControlJoystick joystick;
    private AudioManager audioManager;

    public Rigidbody2D rigi;

    private bool grounded = false;
    private bool rightWalled = false;
    private bool leftWalled = false;
    private float groundRadius = 0.05f;
    public LayerMask whatIsGround;

    private Collider2D playerCollider;
    private Vector2 groundCheckLocation;
    private Vector2 rightWallCheckLocation;
    private Vector2 leftWallCheckLocation;

    public bool isLeftButtonActive;
    public bool isRightButtonActive;
    public bool isJumpButtonActive;

    public bool canPlayerMove = true;

    void Start()
    {
        if (AudioManager.AudioInstance != null)
            audioManager = AudioManager.AudioInstance;

        else
            Debug.LogError("Audiomanager not found. Cannot activate player movement sounds. Error.");

        rigi = GetComponent<Rigidbody2D>();

        playerCollider = GetComponent<Collider2D>();

        TouchController = GameObject.FindGameObjectWithTag("TouchController");

        //If touchcontrol is active, get joystick gameobject as well.
        if (TouchController != null)
            joystick = TouchController.GetComponentInChildren<TouchControlJoystick>();

    }

    void Update()
    {
        //Used for debugging
        if (TouchController == null)
            TouchController = GameObject.FindGameObjectWithTag("TouchController");

        else if (joystick == null && TouchController.activeInHierarchy)
            joystick = TouchController.GetComponentInChildren<TouchControlJoystick>();

        if (!canPlayerMove)
        {
            rigi.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        else
        {
            CreateGroundChecks();

            if (Input.GetKey(KeyCode.LeftArrow))
                HorizontalMovement(-horizontalMovementSpeed);

            else if (Input.GetKey(KeyCode.RightArrow))
                HorizontalMovement(horizontalMovementSpeed);

            else if (joystick != null)
            {
                if (joystick.isJoystickActive)
                {
                    horizontalInput = joystick.HorizontalJoystick();

                    HorizontalMovement(horizontalInput);
                }

                else if (joystick.isJoystickActive == false)
                {
                    HorizontalMovement(0);
                }
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                rigi.velocity = new Vector2(0, rigi.velocity.y);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
    }

    /*
	 *Player turns and moves to right.
	*/
    public void HorizontalMovement(float horizontalInput)
    {
        if (horizontalInput == 0)
            rigi.velocity = new Vector2(0, rigi.velocity.y);

        if (horizontalInput > 0 && !rightWalled)
        {
            rigi.velocity = new Vector2(horizontalInput * baseMovementSpeed, rigi.velocity.y);
            rigi.angularVelocity = -(horizontalInput * rotationalSpeed);
        }

        else if (horizontalInput < 0 && !leftWalled)
        {
            rigi.velocity = new Vector2(horizontalInput * baseMovementSpeed, rigi.velocity.y);
            rigi.angularVelocity = -(horizontalInput * rotationalSpeed);
        }
    }

    public void Jump()
    {
        if (grounded)
        {
            rigi.velocity = new Vector2(rigi.velocity.x, jumpHeight);

            audioManager.Play("JumpSound");
        }
    }

    /*
	 * Creates OverLap circles on bottom of the ball and on both sides of it. Checks whether the sides/bottom is touching objects.
	 *
	*/
    void CreateGroundChecks()
    {
        if (playerCollider == null)
        {
            playerCollider = GetComponent<Collider2D>();
            return;
        }

        groundCheckLocation = rigi.transform.position;
        groundCheckLocation.y -= (playerCollider.bounds.size.y) / 2;
        grounded = Physics2D.OverlapCircle(groundCheckLocation, groundRadius, whatIsGround);

        rightWallCheckLocation = rigi.transform.position;
        rightWallCheckLocation.x += (playerCollider.bounds.size.x) / 2;
        rightWalled = Physics2D.OverlapCircle(rightWallCheckLocation, groundRadius, whatIsGround);

        leftWallCheckLocation = rigi.transform.position;
        leftWallCheckLocation.x -= (playerCollider.bounds.size.x) / 2;
        leftWalled = Physics2D.OverlapCircle(leftWallCheckLocation, groundRadius, whatIsGround);

    }
}
