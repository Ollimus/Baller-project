using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;


public class KillZone : MonoBehaviour {

	private GameObject player;
	private PlayerSpawner playerSpawningpoint;
    private PlayerManager playerManager;
  

	private float gameTime;

	void Start()
	{
        playerManager = PlayerManager.PlayerDataInstance;
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player"))
            KillPlayer(other);
	}

    private void KillPlayer(Collider2D other)
    {
        try
        {
            player = other.transform.gameObject;
            
            //Play animation "Explosion" from animator.
            Animator anim = player.GetComponent<Animator>();
            anim.Play("Explosion");

            player.GetComponent<PlayerMovementController>().DisableMovement();  //If playermovement is not disabled, player can move as animation.

            AudioManager.AudioInstance.Play("PlayerDeath");

            //Get the length of death animation and kill the player after the animation has fully played.
            float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
            Destroy(player, animationLength);

            playerManager.StartPlayerAtLatestCheckpoint();

            playerManager.ReduceLives();
        }

        catch (Exception e)
        {
            Debug.LogError("Error destroying player. Error: " + e);
        }
    }
}