using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    private UIManager UImanager;
    private int playerLives;

	//Set player lives to 5 at start of every screen.
	void Start ()
    {
        playerLives = 3;

        UImanager = GameObject.Find("UIManager").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Reduces player lives by 1. If player does not have lives left, end the game.
    public void ReduceLives()
    {
        try
        {
            playerLives -= 1;

            Debug.Log(playerLives);

            UImanager.RemovePlayerLifeSprite();

            if (playerLives == 0)
                UImanager.ActivateDefeatScreen();
        }

        catch (Exception e)
        {
            Debug.Log("Error with reducing lives. Error: " + e);
        }
    }
}
