using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class KillZone : MonoBehaviour {

	private GameObject player;
	private PlayerSpawningPoint PlayerSpawningPoint;
    private PlayerManager playerManager;

	private float gameTime;

	void Start()
	{
        PlayerSpawningPoint = GameObject.FindGameObjectWithTag("CheckPoints").GetComponentInParent<PlayerSpawningPoint>();
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player"))
		{
            try
            {
                KillPlayer();
                
                PlayerSpawningPoint.doesPlayerExist = false;

                PlayerSpawningPoint.playerDeathTime = (Time.time + PlayerSpawningPoint.respawnTimer);
            }

            catch (Exception e)
            {
                Debug.Log("Error destroying a player. Error: " + e);
            }
		}
	}

    private void KillPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Debug.Log("Player Destroyed");
        Destroy(player);

        playerManager.ReduceLives();
    }
}