using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovementController : MonoBehaviour {

    public float baseMovementSpeed = 3;
	public float rotationalSpeed = 600;
	private float jumpHeight = 8;

	public TouchControlMovement touchControls;

	public Rigidbody2D rigi;
	public CircleCollider2D collider;

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
		GameObject touchObject = GameObject.FindGameObjectWithTag ("TouchController");
		touchControls = touchObject.GetComponent<TouchControlMovement>();
 
        rigi = GetComponent<Rigidbody2D>();
		collider = GetComponent<CircleCollider2D>();
	}


    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log(collider.bounds.size);
            Debug.Log(collider.transform.position);
        }

        CreateGroundChecks();

        if (canPlayerMove)
        { 
            if (rightWalled == false)
            {
                MoveRight();
            }

            if (leftWalled == false)
            {
                MoveLeft();
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
	}

	/*
	 *Player turns and moves to right.
	*/

	private void MoveRight()
	{
		if (Input.GetKey (KeyCode.RightArrow) || isRightButtonActive)
		{
			rigi.velocity = new Vector2 (baseMovementSpeed, rigi.velocity.y);

			rigi.angularVelocity = -rotationalSpeed;
		}
	}

	private void MoveLeft()
	{
		if (Input.GetKey (KeyCode.LeftArrow) || isLeftButtonActive)
		{
			rigi.velocity = new Vector2 (-baseMovementSpeed, rigi.velocity.y);

			rigi.angularVelocity = rotationalSpeed;
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
			groundCheckLocation.y -= (collider.bounds.size.y) / 2;
			grounded = Physics2D.OverlapCircle (groundCheckLocation, groundRadius, whatIsGround);

			rightWallCheckLocation = rigi.transform.position;
			rightWallCheckLocation.x += (collider.bounds.size.x)/2;
			rightWalled = Physics2D.OverlapCircle(rightWallCheckLocation, groundRadius, whatIsGround);

			leftWallCheckLocation = rigi.transform.position;
			leftWallCheckLocation.x -= (collider.bounds.size.x)/2;
			leftWalled = Physics2D.OverlapCircle(leftWallCheckLocation, groundRadius, whatIsGround);
		}

		catch (Exception e)
		{
			Debug.Log("Location checks failed. Error: " + e);
		}
	}
}
