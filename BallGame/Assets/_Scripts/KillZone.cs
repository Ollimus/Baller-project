using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;


public class KillZone : MonoBehaviour {

	private GameObject player;
	private PlayerSpawningPoint playerSpawningpoint;
    private PlayerManager playerManager;

    private AudioSource explosionAudio;

	private float gameTime;

	void Start()
	{
        playerSpawningpoint = GameObject.FindGameObjectWithTag("StartingPoint").GetComponentInParent<PlayerSpawningPoint>();
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
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
            playerSpawningpoint.doesPlayerExist = false;

            playerSpawningpoint.playerDeathTime = (Time.time + playerSpawningpoint.respawnTimer);
        }

        catch(Exception e)
        {
            Debug.LogError("Error setting up player dead. Error: " + e);
        }
    }

    private void KillPlayer(Collider2D other)
    {
        try
        {
            player = other.transform.gameObject;
            
            //Play animation "Explosion" from animator.
            Animator anim = player.GetComponent<Animator>();
            anim.Play("Explosion");

            FindObjectOfType<AudioManager>().Play("PlayerDeath");

            float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;

            Destroy(player, animationLength);

            StartCoroutine(playerSpawningpoint.SpawnPlayerAtCheckpoint());

            playerManager.ReduceLives();
        }

        catch (Exception e)
        {
            Debug.LogError("Error destroying player. Error: " + e);
        }
    }
}