using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControlMovement : MonoBehaviour {
	
	PlayerMovementController player;

	public bool jumpCheck;

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

	public void touchControlRight()
	{
		player.isRightButtonActive = true;
	}

	public void touchControlLeft()
	{
		player.isLeftButtonActive = true;
	}

	public void releaseRightKey()
	{
		player.isRightButtonActive = false;
	}

	public void releaseLeftKey()
	{
		player.isLeftButtonActive = false;
	}

	public void touchControlJump()
	{
		jumpCheck = true;

		player.isJumpButtonActive = true;
	}

	public void releaseJump()
	{
		jumpCheck = true;
		player.isJumpButtonActive = false;
	}
}
