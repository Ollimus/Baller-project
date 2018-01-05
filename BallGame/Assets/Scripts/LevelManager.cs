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
        scene = SceneManager.GetActiveScene();
        UnPauseGame();
    }

    //Makes sure game has been unpaused when loaded into another level.
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
    //Takes current level index in Start (ie. Level 1 = index 1) and adds 1 to that (ie. Level 2 is index 2).
    public void NextLevel()
    {
        try
        {
            SceneManager.LoadScene(scene.buildIndex + 1);
        }

        catch (Exception e)
        {
            Debug.Log("Error changing to the next level. Error: " + e);
        }
    }

    //Loads the same scene again.
    public void RetryLevel()
    {
        try
        {
            SceneManager.LoadScene(scene.buildIndex);
        }

        catch (Exception e)
        {
            Debug.Log("Error retrying level. Returning to Main Menu. Error: " + e);
            ChangeScene("00_MainMenu");
        }
    }
}
