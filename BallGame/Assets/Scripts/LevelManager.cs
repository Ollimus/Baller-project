using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{

    Scene scene;
    int sceneNumber;

    private void Start()
    {
        UnPauseGame();
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1f;
    }


    //Change to specific scene using level name as variable
    public void ChangeScene(string levelName)
    {
        try
        {
            Debug.Log("Changing level to: " + levelName);
            SceneManager.LoadScene(levelName);
        }

        catch (Exception e)
        {
            Debug.Log("Error changing level: " + e);
        }


    }

    //Automatically go to the next scene
    public void NextLevel()
    {
        try
        {
            scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex + 1);
        }

        catch (Exception e)
        {
            Debug.Log("Error changing to the next level. Error: " + e);
        }
    }
}
