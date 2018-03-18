using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;


public class KillZone : MonoBehaviour {

	private GameObject player;
	private PlayerSpawningPoint PlayerSpawningPoint;
    private PlayerManager playerManager;

    private float respawnTime;

	private float gameTime;

	void Start()
	{
        PlayerSpawningPoint = GameObject.FindGameObjectWithTag("StartingPoint").GetComponentInParent<PlayerSpawningPoint>();
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        respawnTime = PlayerSpawningPoint.respawnTimer;
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player"))
		{
            KillPlayer(other);

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

    private void KillPlayer(Collider2D other)
    {
        try
        {
            player = other.transform.gameObject;

            Debug.Log("Player Destroyed");

            Animator anim = player.GetComponent<Animator>();
            anim.Play("Explosion");

            AnimatorStateInfo currInfo = anim.GetCurrentAnimatorStateInfo(0);

            float animationLength = currInfo.length;

            Destroy(player, animationLength);

            playerManager.ReduceLives();
        }

        catch (Exception e)
        {
            Debug.Log("Error destroying player. Error: " + e);
        }
    }
}