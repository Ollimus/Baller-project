using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    private UIManager UIManager;

    private int lifeAmount = 5;

	// Use this for initialization
	void Start ()
    {
        try
        {
            UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        }

        catch (Exception e)
        {
            Debug.Log("Error finding UIManager. Add UImanager to the scene. Error: " + e);
        }
	}

    //Reduce player lives by 1 after death.
    public void ReducePlayerLives()
    {
        lifeAmount -= 1;

        /*
         * Create a function in UImanager that handles life tracking in game UI.
        */
    }
}
