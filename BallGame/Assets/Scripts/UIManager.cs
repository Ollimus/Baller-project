using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        //Gameobjects to affected by UIManager
        private GameObject informationObject;
        private GameObject victoryMenu;
        private GameObject pauseMenu;
        private GameObject playerLives;
        private GameObject defeatMenu;

        private GameObject menuObject;
        private GameObject[] menus;

        private Button button;
        private GameObject[] ButtonArray;

        private GameObject touchControls;
        public bool testButtonFunctionability = false;

        private Scene scene;
        private LevelManager levelManager;
        private AudioManager audioManager;

        private List<GameObject> playerLifeSpriteList = new List<GameObject>();
        private Text informationText;
        private Text completionTimeText;
        private IEnumerator coroutine;
        private string operatingSystemCheck;

        void Awake()
        {
            try
            {
                levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
                informationObject = GameObject.Find("InformationText");
                touchControls = GameObject.FindGameObjectWithTag("TouchButtons");
                audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            }

            catch (Exception e)
            {
                Debug.Log("Error setting up managers. Error: " + e);
            }

            //Ignore setting up gameplay elements if active scene is main menu.
            scene = SceneManager.GetActiveScene();

            if (scene.name != "00_MainMenu")
            {
                try
                {
                    menuObject = GameObject.FindGameObjectWithTag("Menu");

                    playerLifeSpriteList = GameObject.FindGameObjectsWithTag("PlayerLives").OrderBy(go => go.name).ToList();

                    /*
                     *Edit this to be better
                    */
                    victoryMenu = menuObject.transform.GetChild(0).gameObject;
                    pauseMenu = menuObject.transform.GetChild(1).gameObject;
                    defeatMenu = menuObject.transform.GetChild(2).gameObject;

                    menus = new GameObject[3];

                    for (int i = 0; i < 3; i++)
                    {
                        menus[i] = menuObject.transform.GetChild(i).gameObject;
                    }
                }

                catch (Exception e)
                {
                    Debug.Log("Error setting up gameplay elements. Error: " + e);
                }
            }
        }

        private void Start()
        {
            GameObject[] unFinishedButton = GameObject.FindGameObjectsWithTag("UnfinishedButton");

            foreach (GameObject obj in unFinishedButton)
            {
                Button button = obj.GetComponent<Button>();

                button.onClick.AddListener(() => PlaceHolderText());
            }
        }

        /*
         *Done in Awake to make sure the player does not see the menus pop up.
         *Menus need to be activated, because non-activated gameobjects can't be found.
        */
        public void ActivateMenuButtons(string button)
        {
            try
            {
                ButtonArray = GameObject.FindGameObjectsWithTag(button);

                levelManager.InitiateButtons(ButtonArray);
            }

            catch (Exception e)
            {
                Debug.Log("Error activating/deactiving menus and fetching buttons. Error: " + e);
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

            if (testButtonFunctionability == true && (victoryMenu || defeatMenu || pauseMenu))
            {
                ActivateMenuButtons("Button");
                testButtonFunctionability = false;
            }
        }

        //Handles activation of End Menu
        public void ActivateVictoryScreen(string completionTime)
        {
            try
            {
                completionTimeText = victoryMenu.transform.Find("txtCompletionTime").GetComponentInChildren<Text>();
                completionTimeText.text = completionTime;
                victoryMenu.SetActive(true);

                ActivateMenuButtons("Button");
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
                defeatMenu.SetActive(true);
                ActivateMenuButtons("Button");

                Time.timeScale = 0f;
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

                ActivateMenuButtons("Button");

                audioManager.MuteAudio();

                levelManager.PauseGame();
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

                audioManager.UnmuteAudio();

                levelManager.UnPauseGame();
            }

            catch (Exception e)
            {
                Debug.Log("Error Resuming Game. Error: " + e);
            }

        }

        //Handles sending information for player in UI.
        public void ShowInformationText(string inputText)
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
                Destroy(playerLifeSpriteList[0]);
                playerLifeSpriteList.RemoveAt(0);
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
    }
}
