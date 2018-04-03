using UnityEngine;
using System;

public class PlayerMovementController : MonoBehaviour {

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

	private Vector2 groundCheckLocation;
	private Vector2 rightWallCheckLocation;
	private Vector2 leftWallCheckLocation;

	public bool isLeftButtonActive;
	public bool isRightButtonActive;
	public bool isJumpButtonActive;

    public bool canPlayerMove = true;

    void Start ()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        rigi = GetComponent<Rigidbody2D>();

        TouchController = GameObject.FindGameObjectWithTag("TouchController");

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


        if (canPlayerMove)
        {
            CreateGroundChecks();

            if (Input.GetKey(KeyCode.LeftArrow))
                HorizontalMovement(-horizontalMovementSpeed);

            else if (Input.GetKey(KeyCode.RightArrow))
                HorizontalMovement(horizontalMovementSpeed);

            else if (joystick != null && joystick.isJoystickActive)
            {
                horizontalInput = joystick.HorizontalJoystick();

                HorizontalMovement(horizontalInput);
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

        else
        {
            rigi.constraints = RigidbodyConstraints2D.FreezeAll;
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
        try
        {
            if (grounded)
            {
                rigi.velocity = new Vector2(rigi.velocity.x, jumpHeight);

                audioManager.Play("JumpSound");
            }
        }

        catch (Exception e)
        {
            Debug.Log("Error jumping: " + e);
        }
    }

	/*
	 * Creates OverLap circles on bottom of the ball and on both sides of it. Checks whether the sides/bottom is touching objects.
	 *
	*/
	void CreateGroundChecks()
	{
		try
		{
			groundCheckLocation = rigi.transform.position;
			groundCheckLocation.y -= (GetComponent<Collider2D>().bounds.size.y) / 2;
			grounded = Physics2D.OverlapCircle (groundCheckLocation, groundRadius, whatIsGround);

			rightWallCheckLocation = rigi.transform.position;
			rightWallCheckLocation.x += (GetComponent<Collider2D>().bounds.size.x)/2;
			rightWalled = Physics2D.OverlapCircle(rightWallCheckLocation, groundRadius, whatIsGround);

			leftWallCheckLocation = rigi.transform.position;
			leftWallCheckLocation.x -= (GetComponent<Collider2D>().bounds.size.x)/2;
			leftWalled = Physics2D.OverlapCircle(leftWallCheckLocation, groundRadius, whatIsGround);
		}

		catch (Exception e)
		{
			Debug.Log("Location checks failed. Error: " + e);
		}
	}
}
