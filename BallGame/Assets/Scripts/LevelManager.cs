﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    //SceneManager scene;

    //public string levelName;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}