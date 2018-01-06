using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {

    //Gameobjects to affected by UIManager
    public GameObject menuScreen;
    public GameObject informationObject;
    public GameObject EndLevelMenu;
    public GameObject pauseMenu;
    public GameObject playerLives;
    public GameObject defeatScreen;

    public GameObject touchControls;

    private GameObject playerLifeSprite;
    private Text informationText;
    private Text completionTimeText;
    private bool isActivated = false;
    private IEnumerator coroutine;
    private string operatingSystemCheck;

    void Start()
    {
        //Receives and sets up information about user's operating system.
        operatingSystemCheck = SystemInfo.operatingSystem;

        //Checks whether scene has touchcontrols set up.
        if (touchControls != null)
        {
            //If user runs the game on windows/mac, disables touch controls. Otherwise activates them (for Android/IOS).
            if (operatingSystemCheck.StartsWith("Windows") || operatingSystemCheck.StartsWith("Mac"))
            {
                touchControls.SetActive(false);
                //exitMenuButton.SetActive(false);
            }

            else
            {
                touchControls.SetActive(true);
                //exitMenuButton.SetActive(true);
            }
        }
    }

    private void Update()
    {
        //If player uses Escape, check whether pausemenu is actives. If not active, create menu and pause. If active, resume game.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeInHierarchy)
                ActivatePauseMenu();

            else
                ResumeGame();
        }
    }

    //Handles activation of End Menu
    public void ActivateVictoryScreen(string completionTime)
    {
        if (!isActivated)
        {
            try
            {
                isActivated = true;
                menuScreen.transform.SetSiblingIndex(-1);
                completionTimeText = EndLevelMenu.transform.Find("txtCompletionTime").GetComponentInChildren<Text>();
                completionTimeText.text = completionTime;

                menuScreen.SetActive(true);
                EndLevelMenu.SetActive(true);
            }

            catch (Exception e)
            {
                Debug.Log("Error creating victory menu for player. Error: " + e);
            }
        }
    }

    public void ActivateDefeatScreen()
    {
        try
        {
            menuScreen.SetActive(true);
            defeatScreen.SetActive(true);

            PauseGame();
        }

        catch (Exception e)
        {
            Debug.Log("Error starting defeat menu. Error: " + e);
        }
    }

    //Activates pause menu and pauses the game
    public void ActivatePauseMenu()
    {
        try
        {
            menuScreen.SetActive(true);
            pauseMenu.SetActive(true);

            PauseGame();
        }

        catch (Exception e)
        {
            Debug.Log("Error pausing game. Error: " + e);
        }
    }

    //Resumes the game by removing menus and unpausing the game
    public void ResumeGame()
    {
        try
        {
            menuScreen.SetActive(false);
            pauseMenu.SetActive(false);

            UnPauseGame();
        }

        catch (Exception e)
        {
            Debug.Log("Error Resuming Game. Error: " + e);
        }

    }

    //Handles sending information for player in UI.
    public void ShowInformationText(string inputText)
    {
        if (!informationObject.activeInHierarchy)
        {
            try
            {
                informationText = informationObject.GetComponent<Text>();
                informationText.text = inputText;
                informationObject.SetActive(true);
                coroutine = FlashUIText(2.0f);
                StartCoroutine(coroutine);
            }

            catch (Exception e)
            {
                Debug.Log("Error with inputting text to player. Error: " + e);
            }
        }
    }

    //Makes the text disappear after a while
    private IEnumerator FlashUIText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        informationObject.SetActive(false);
    }

    //Disables touch controls, mainly used to be called when game is used on android/ios when the game ends and activates end-game screen or player opens menu.
    public void DisableTouchControl()
    {
        touchControls.SetActive(false);
    }

    //Finds a sprite tagged PlayerLives and deletes it.
    public void RemovePlayerLifeSprite()
    {
       try
        {
            playerLifeSprite = GameObject.FindGameObjectWithTag("PlayerLives");

            GameObject.Destroy(playerLifeSprite);
        }

        catch (Exception e)
        {
            Debug.Log("Error removing a player life from UI. Error: " + e);
        }
    }

    //PlaceHolderText for unfinished functions.
    public void PlaceHolderText()
    {
        String placeholderText = "Functionality under construction!";

        ShowInformationText(placeholderText);
    }

    //Make for clarity sake. Pauses and unpauses game time.
    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1f;
    }
}
