﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {

    public GameObject menuScreen;
    private Text completionTimeText;
    private bool isActivated = false;

    /*// Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frames
	void Update ()
    {
	}*/

    public void ActivateMenu(string completionTime)
    {
        if (!isActivated)
        {
            try
            {
                isActivated = true;
                //menuScreen.transform.position = new Vector3(0,0,1);
                menuScreen.transform.SetSiblingIndex(-1);
                completionTimeText = menuScreen.transform.FindChild("txtCompletionTime").GetComponentInChildren<Text>();
                completionTimeText.text = "You are victorious! You completed the level with " + completionTime;
                Debug.Log(completionTimeText.text);
                menuScreen.SetActive(true);
            }

            catch (Exception e)
            {
                Debug.Log("Error creating victory menu for player. Error: " + e);
            }
        }
    }
}