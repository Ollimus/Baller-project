using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

	private GameObject player;
	private PlayerSpawningPoint PlayerSpawningPoint;

	private float gameTime;

	void start()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player"))
		{
			GameObject findCheckPoints = GameObject.FindGameObjectWithTag ("CheckPoints");
			PlayerSpawningPoint = findCheckPoints.GetComponentInParent<PlayerSpawningPoint> ();

			player = GameObject.FindGameObjectWithTag ("Player");

			Debug.Log ("Player Destroyed");
			Destroy (player);

			PlayerSpawningPoint.doesPlayerExist = false;

            PlayerSpawningPoint.playerDeathTime = (Time.time + PlayerSpawningPoint.respawnTimer);
		}
	}
}