using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControlMovement : MonoBehaviour, IPointerDownHandler
{
	
	PlayerMovementController player;

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

    public virtual void OnPointerDown(PointerEventData data)
    {
        player.Jump();
    }

    public void TouchControlJump()
	{
		player.isJumpButtonActive = true;
	}

	public void ReleaseJump()
	{
		player.isJumpButtonActive = false;
	}
}
