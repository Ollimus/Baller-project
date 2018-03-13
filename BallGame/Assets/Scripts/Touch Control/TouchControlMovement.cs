using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControlMovement : MonoBehaviour {
	
	PlayerMovementController player;

	//public bool jumpCheck;

	void Start()
	{
		/*GameObject playerObject = GameObject.FindGameObjectWithTag ("Player");
		player = playerObject.GetComponent<PlayerMovementController>();*/
	}

	void Update()
	{
		GameObject playerObject = GameObject.FindGameObjectWithTag ("Player");

		if (playerObject != null)
		{
			player = playerObject.GetComponent<PlayerMovementController>();
		}
	}

	public void TouchControlJump()
	{
		//jumpCheck = true;

		player.isJumpButtonActive = true;
	}

	public void ReleaseJump()
	{
		//jumpCheck = true;
		player.isJumpButtonActive = false;
	}
}
