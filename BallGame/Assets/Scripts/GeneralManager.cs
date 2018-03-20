using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GeneralManager : MonoBehaviour
{
    Scene scene;

    public bool disableGameFunctions = false;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name == "00_MainMenu")
        {
            disableGameFunctions = true;
        }

        else
        {
            disableGameFunctions = false;
        }
    }

    // Use this for initialization
    void Start ()
    {
        GameObject checkpoints = GameObject.Find("Checkpoints");

        if (checkpoints == null)
            throw new Exception("There is no Checkpoints parent object and possibly does not contain checkpoints!");

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
