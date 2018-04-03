using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControlMovement : MonoBehaviour, IPointerDownHandler
{
    PlayerMovementController player;

    public virtual void OnPointerDown(PointerEventData data)
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
        }            

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
