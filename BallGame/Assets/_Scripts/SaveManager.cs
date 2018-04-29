using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public static SaveManager SaveManagerInstance;

    public float ResetLevelsBeforeThisVersion; //Make it private

    //Variables used for data saves.
    private int unlockedSkins = 1;
    public int unlockedLevels = 1;
    public int specialSkins;
    private bool resetPlayerProgress = false;
    private bool resetLevels = false;
    public int levelFinishTime;

    private void Awake()
    {
        if (SaveManagerInstance == null)
            SaveManagerInstance = this;

        else
        {
            Destroy(gameObject);
            return;
        }

        LoadSaves();

        /*
         * Makes sure PlayerDataManager  is not a child of anything, because a child cannot be indestructable.
        */
        if (gameObject.transform.parent != null)
        {
            gameObject.transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            return;
        }
    }

    private void Start()
    {
        CheckVersion();
    }

    private void CheckVersion()
    {
        float versionNumber = float.Parse(Application.version);

        if (versionNumber < ResetLevelsBeforeThisVersion)
        {
            PlayerPrefs.DeleteKey("Levels");
        }
    }

    private void LoadSaves()
    {
        unlockedLevels = PlayerPrefs.GetInt("Levels", 1);   //Default starting level is 1. Levelmanager checks by level whether you can start a level.
        unlockedSkins = PlayerPrefs.GetInt("Skins", -1);
        specialSkins = PlayerPrefs.GetInt("specialSkins", -1);
    }

    //Unlocks the next level and saves it.
    public void UnlockNewLevel()
    {
        unlockedLevels++;

        PlayerPrefs.SetInt("Levels", unlockedLevels);
    }

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("Levels", unlockedLevels);
        PlayerPrefs.SetInt("UnlockableSkins", unlockedSkins);
        PlayerPrefs.SetInt("SpecialSkins", specialSkins);
    }
}
