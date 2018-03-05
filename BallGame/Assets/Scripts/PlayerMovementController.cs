using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovementController : MonoBehaviour {

    public float baseMovementSpeed = 6;
	public float rotationalSpeed = 600;
	private float jumpHeight = 8;
    private float horizontalInput;

	public TouchControlMovement touchControls;
    private TouchControlJoystick joystick;

	public Rigidbody2D rigi;
	//public CircleCollider2D collider;

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
		touchControls = GameObject.FindGameObjectWithTag("TouchController").GetComponent<TouchControlMovement>();
        joystick = GameObject.FindGameObjectWithTag("TouchButtons").GetComponentInChildren<TouchControlJoystick>();

        Debug.Log(joystick);

        rigi = GetComponent<Rigidbody2D>();
        //collider = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        CreateGroundChecks();

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = Input.GetAxis("Horizontal");

            HorizontalMovement(horizontalInput);
        }

        else if (joystick != null && joystick.isJoystickActive)
        {
            horizontalInput = joystick.HorizontalJoystick();

            HorizontalMovement(horizontalInput);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigi.velocity = new Vector2(0, rigi.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || isJumpButtonActive)
        {
            try
            {
                if (grounded)
                {
                    rigi.velocity = new Vector2(rigi.velocity.x, jumpHeight);
                }
            }

            catch (Exception e)
            {
                Debug.Log("Error jumping: " + e);
            }
        }
    }

	/*
	 *Player turns and moves to right.
	*/
	public void HorizontalMovement(float horizontalInput)
	{
        CreateGroundChecks();

        if (rightWalled == false || leftWalled == false)
        {
            rigi.velocity = new Vector2(horizontalInput * baseMovementSpeed, rigi.velocity.y);
            rigi.angularVelocity = -(horizontalInput * rotationalSpeed);
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
