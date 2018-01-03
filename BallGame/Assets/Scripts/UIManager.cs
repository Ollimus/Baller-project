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

    public GameObject touchControls;
    public GameObject exitMenuButton;

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
                exitMenuButton.SetActive(false);
            }

            else
            {
                touchControls.SetActive(true);
                exitMenuButton.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivatePauseMenu();
        }
    }

    //Handles activation of End Menu
    public void ActivateMenu(string completionTime)
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

    private IEnumerator FlashUIText(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        informationObject.SetActive(false);
    }

    //Quits from application.
    public void QuitApplication()
    {
        Application.Quit();
    }

    //Disables touch controls, mainly used to be called when game is used on android/ios when the game ends and activates end-game screen or player opens menu.
    public void DisableTouchControl()
    {
        touchControls.SetActive(false);
    }

    //PlaceHolderText for unfinished functions.
    public void PlaceHolderText()
    {
        String placeholderText = "Functionality under construction!";

        ShowInformationText(placeholderText);
    }

    public void ActivatePauseMenu()
    {
        
    }

    public void HidePauseMenu()
    {

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
