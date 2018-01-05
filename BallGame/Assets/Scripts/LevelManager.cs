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

    //Automatically go to the next scene when "Next level" -button is pressed.
    public void NextLevel()
    {
        try
        {
            scene = SceneManager.GetActiveScene();

           // if (SceneManager.GetSceneByBuildIndex(scene.buildIndex + 1) != null)
                SceneManager.LoadScene(scene.buildIndex + 1);

            //else
             //   Debug.Log("Error loading into next level. Does not exist in build.");
        }

        catch (Exception e)
        {
            Debug.Log("Error changing to the next level. Error: " + e);
        }
    }
}
