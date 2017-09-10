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

	bool grounded = false;
	bool rightWalled = false;
	bool leftWalled = false;
	float groundRadius = 0.05f;
	public LayerMask whatIsGround;

	private Vector2 groundCheckLocation;
	private Vector2 rightWallCheckLocation;
	private Vector2 leftWallCheckLocation;

	public bool isLeftButtonActive;
	public bool isRightButtonActive;
	public bool isJumpButtonActive;

	void Start ()
    {
		GameObject touchObject = GameObject.FindGameObjectWithTag ("TouchController");
		touchControls = touchObject.GetComponent<TouchControlMovement>();
 
        rigi = GetComponent<Rigidbody2D>();
		collider = GetComponent<CircleCollider2D>();
	}


	void Update ()
    {
		if (Input.GetKey(KeyCode.E))
		{
			Debug.Log (collider.bounds.size);
			Debug.Log (collider.transform.position);
		}
			
		createGroundChecks();

		if (rightWalled == false)
		{
			moveRight();
		}

		if (leftWalled == false)
		{
			moveLeft ();
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			rigi.velocity = new Vector2 (0, rigi.velocity.y);
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) || isJumpButtonActive)
		{
			try
			{
				//if (grounded || (leftWalled || rightWalled))
				if (grounded)
				{				
					rigi.velocity = new Vector2 (rigi.velocity.x, jumpHeight);
				}
			}

			catch (Exception e)
			{
				Debug.Log ("Error jumping: " + e);
			}
		}
	}

	/*
	 *Player turns and moves to right.
	*/

	private void moveRight()
	{
		if (Input.GetKey (KeyCode.RightArrow) || isRightButtonActive)
		{
			if (grounded == true)
			{
				//if (rigi.angularVelocity < 0)
				//{
					//rigi.angularVelocity = 0;
					rigi.velocity = new Vector2 (baseMovementSpeed, rigi.velocity.y);
				//}

				//else
				//{
					rigi.angularVelocity = -rotationalSpeed;
				//}
			}

			else if ((rigi.velocity.y > 0 || rigi.velocity.y < 0) && grounded == false)
			{
				rigi.velocity = new Vector2 (baseMovementSpeed, rigi.velocity.y);

				rigi.angularVelocity = -rotationalSpeed;
				/*if (Math.Round (groundCheckLocation.x) == Math.Round (rigi.transform.position.x))
						rigi.angularVelocity = -movementSpeedTest;*/
			}
		}
	}

	private void moveLeft()
	{
		if (Input.GetKey (KeyCode.LeftArrow) || isLeftButtonActive)
		{

			if (grounded == true)
			{
				//if (rigi.angularVelocity > 0)
				//{
					//rigi.angularVelocity = 0;
					rigi.velocity = new Vector2 (-baseMovementSpeed, rigi.velocity.y);
				//}

				//else
				//{
				rigi.angularVelocity = rotationalSpeed;
				//}
					
			}

			else if ((rigi.velocity.y > 0 || rigi.velocity.y < 0) && grounded == false)
			{
				rigi.velocity = new Vector2 (-baseMovementSpeed, rigi.velocity.y);

				rigi.angularVelocity = rotationalSpeed;
			}
		}
	}

	/*
	 * Creates OverLap circles on bottom of the ball and on both sides of it. Checks whether the sides/bottom is touching objects.
	 *
	*/
	void createGroundChecks()
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
