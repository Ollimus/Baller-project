using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAbilities : MonoBehaviour {

    //private GameObject playerObject;
    //private PlayerMovementController playerMovementScript;

    private PlayerMovementController playerMovement;

	private bool isAbilityActive;

	public bool isEnlargeActive;
	private bool isReductionActive;
	public bool isClimbActive;

	public Vector2 playerBaseSize;

	public float enlargeAbilitySize = 2;
	public float shrinkAbilitySize = 2;

	private float time;
	private float abilityCooldown = 5;

    private Rigidbody2D rigi; 


	void Start ()
	{
        rigi = GetComponent<Rigidbody2D>();
        playerBaseSize = rigi.transform.localScale;

        playerMovement = GetComponent<PlayerMovementController>();
    }
		
	void Update ()
	{

       // playerMovement.Test();

		if (isAbilityActive == false)
		{	
			try
			{
				if ((Input.GetKey (KeyCode.A) || isEnlargeActive) && isAbilityActive == false)
				{

					EnlargeAbility();
				}

				if ((Input.GetKey(KeyCode.S) || isReductionActive) && isAbilityActive == false)
				{
					SizeReductionAbility();
				}
			}

			catch (Exception e)
			{
				Debug.Log ("Error activating ability: " + e);
			}
		}

		if (Time.time >= time + abilityCooldown && isAbilityActive == true)
		{
			ResetPlayer();
			isAbilityActive = false;
		}
	}

	private void ActivateAbilityTimer()
	{
		isAbilityActive = true;

		time = Time.time;
	}

	private void ResetPlayer()
	{
        rigi.transform.localScale = playerBaseSize;
    }

	private void EnlargeAbility()
	{
		ActivateAbilityTimer();

		Debug.Log ("Enlarge ability. Enlarging player.");
        rigi.transform.localScale = new Vector2(enlargeAbilitySize, enlargeAbilitySize);
    }

	private void SizeReductionAbility()
	{
		ActivateAbilityTimer();
        rigi.transform.localScale = new Vector2(enlargeAbilitySize, enlargeAbilitySize);
    }

	public void climbAblity()
	{
	}

	/*
	public void touchCtrlEnlargeAbility()
	{

	}

	public void touchCtrlEnlargeAbility()
	{

	}

	public void touchCtrlEnlargeAbility()
	{

	}*/
}
