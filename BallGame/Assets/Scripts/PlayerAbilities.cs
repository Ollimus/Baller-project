using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAbilities : MonoBehaviour {
	
	private PlayerMovementController player;

	private bool isAbilityActive;

	public bool isEnlargeActive;
	private bool isReductionActive;
	public bool isClimbActive;

	public Vector2 playerBaseSize;

	public float enlargeAbilitySize = 10;
	public float shrinkAbilitySize = 2;

	private bool test;

	private float time;
	private float abilityCooldown = 5;


	void Start ()
	{
		GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
		player = playerObject.GetComponent<PlayerMovementController>();

		playerBaseSize = player.transform.localScale;
	}
		
	void Update ()
	{
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
		player.transform.localScale = playerBaseSize;
	}

	private void EnlargeAbility()
	{
		ActivateAbilityTimer();

		Debug.Log ("Enlarge ability. Enlarging player.");
		player.transform.localScale = new Vector2(enlargeAbilitySize, enlargeAbilitySize);
	}

	private void SizeReductionAbility()
	{
		ActivateAbilityTimer();

		player.transform.localScale = new Vector2(shrinkAbilitySize, shrinkAbilitySize);
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
