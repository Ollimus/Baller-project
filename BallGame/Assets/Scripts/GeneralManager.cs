using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GeneralManager : MonoBehaviour
{
    public bool disableGameFunctions = false;

    public void DisableManager(GameObject manager)
    {
        manager.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {
        if (SceneManager.GetActiveScene().name == "00_MainMenu")
        {
            disableGameFunctions = true;
        }

        else
        {
            CheckIfPlayable();

            disableGameFunctions = false;
        }
	}

    void CheckIfPlayable()
    {
        GameObject checkpoints = GameObject.Find("Checkpoints");

        if (checkpoints == null)
            throw new Exception("There is no Checkpoints parent object and possibly does not contain checkpoints!");
    }
}
