using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
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
}
