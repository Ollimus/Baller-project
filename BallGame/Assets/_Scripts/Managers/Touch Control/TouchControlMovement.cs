using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControlMovement : MonoBehaviour, IPointerDownHandler
{
    PlayerMovementController player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
    }

    public virtual void OnPointerDown(PointerEventData data)
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();

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
