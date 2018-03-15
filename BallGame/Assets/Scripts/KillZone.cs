using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;


public class KillZone : MonoBehaviour {

	private GameObject player;
	private PlayerSpawningPoint PlayerSpawningPoint;
    private PlayerManager playerManager;

	private float gameTime;

	void Start()
	{
        PlayerSpawningPoint = GameObject.FindGameObjectWithTag("StartingPoint").GetComponentInParent<PlayerSpawningPoint>();
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player"))
		{
            KillPlayer();

            SetPlayerDead();
		}
	}

    void SetPlayerDead()
    {
        try
        {
            PlayerSpawningPoint.doesPlayerExist = false;

            PlayerSpawningPoint.playerDeathTime = (Time.time + PlayerSpawningPoint.respawnTimer);
        }

        catch(Exception e)
        {
            Debug.Log("Error setting up player dead. Error: " + e);
        }
    }

    private void KillPlayer()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");

            Debug.Log("Player Destroyed");
            Destroy(player);

            //player.GetComponent<Animation>().Play("Explosion");

            playerManager.ReduceLives();
        }

        catch (Exception e)
        {
            Debug.Log("Error destroying player. Error: " + e);
        }
    }
}