using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Managers;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        //Gameobjects to affected by UIManager
        public GameObject informationObject;
        public GameObject endLevelMenu;
        public GameObject pauseMenu;
        public GameObject playerLives;
        public GameObject defeatScreen;

        private Button button;
        private GameObject[] ButtonArray;

        public GameObject touchControls;
        public bool testButtons = false;

        private LevelManager levelManager;

        private GameObject playerLifeSprite;
        private Text informationText;
        private Text completionTimeText;
        private IEnumerator coroutine;
        private string operatingSystemCheck;

        void Start()
        {
            //Checks whether scene has touchcontrols set up.
            /*if (touchControls != null)
            {
                //Receives and sets up information about user's operating system.
                operatingSystemCheck = SystemInfo.operatingSystem;

                //If user runs the game on windows/mac, disables touch controls. Otherwise activates them (for Android/IOS).
                if (operatingSystemCheck.StartsWith("Windows") || operatingSystemCheck.StartsWith("Mac"))
                {
                    touchControls.SetActive(false);
                }

                else
                {
                    touchControls.SetActive(true);
                }
            }*/

            levelManager = GameObject.Find("SceneManager").GetComponent<LevelManager>();
        }

        /*
         *Done in Awake to make sure the player does not see the menus pop up.
         *Menus need to be activated, because non-activated gameobjects can't be found.
        */
        private void ActivateMenuButtons()
        {
            try
            {
                ButtonArray = GameObject.FindGameObjectsWithTag("Button");

                levelManager.InitiateButtons(ButtonArray);
            }

            catch (Exception e)
            {
                Debug.Log("Error activating/deactiving menus and fetching buttons. Eror: " + e);
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

            if (testButtons == true && (endLevelMenu || defeatScreen || pauseMenu))
            {
                ActivateMenuButtons();
                testButtons = false;
            }


        }

        //Handles activation of End Menu
        public void ActivateVictoryScreen(string completionTime)
        {
            try
            {
                completionTimeText = endLevelMenu.transform.Find("txtCompletionTime").GetComponentInChildren<Text>();
                completionTimeText.text = completionTime;
                endLevelMenu.SetActive(true);

                ActivateMenuButtons();
            }

            catch (Exception e)
            {
                 Debug.Log("Error creating victory menu for player. Error: " + e);
            }
        }

        public void ActivateDefeatScreen()
        {
            try
            {
                defeatScreen.SetActive(true);
                ActivateMenuButtons();
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
                pauseMenu.SetActive(false);

                ActivateMenuButtons();

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
}
