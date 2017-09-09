/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	PlayerMovementController player;

	void Start ()
	{
		GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
		player = playerObject.GetComponent<PlayerMovementController>();

	}

	// Update is called once per frame
	void Update ()
	{
		if (player.rigi.velocity.x > 0 || player.rigi.velocity.x < 0 )
		{
			movementAnimation();
		}
	}

	public void movementAnimation()
	{
		if (player.rigi.velocity.x > 0)
		{
			//Forward movement
			//sprite.transform.Rotate(rigi.velocity.x, rigi.velocity.y, 0, Space.Self);
			player.sprite.transform.Rotate(Time.deltaTime, 0, 0, Space.Self);

		}

		if (player.rigi.velocity.x < 0)
		{
			//Backwards movement
			player.sprite.transform.Rotate(Time.deltaTime, 0, 0);
		}
	}
}
*/