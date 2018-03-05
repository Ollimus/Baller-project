using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	GameObject player;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void LateUpdate ()
	{
		if (player == null)
			player = GameObject.FindGameObjectWithTag ("Player");

		else if (player != null)
			this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}
}
