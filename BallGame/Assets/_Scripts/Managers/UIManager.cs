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
        private ShowSongName songUIText;

        [HideInInspector]
        public GameObject pauseMenu;
        private GameObject playerLives;
        private GameObject defeatMenu;
        private GameObject victoryMenu;

        private GameObject menuObject;
        private GameObject[] menus;

        private Button button;
        private GameObject[] ButtonArray;

        private Scene scene;
        private LevelManager levelManager;
        private AudioManager audioManager;
        private SaveManager saveManager;

        private GameObject touchControls;

        [HideInInspector]
        public List<GameObject> playerLifeSpriteList = new List<GameObject>();
        private Text informationText;
        private Text completionTimeText;
        private IEnumerator coroutine;
        private string operatingSystemCheck;
        private string sceneName;

        private void Start()
        {
            try
            {
                levelManager = gameObject.transform.parent.GetComponentInChildren<LevelManager>();
                informationObject = GameObject.Find("InformationText");

                if (SaveManager.SaveManagerInstance != null)
                    saveManager = SaveManager.SaveManagerInstance;

                if (AudioManager.AudioInstance != null)
                    audioManager = AudioManager.AudioInstance;
            }

            catch (Exception e)
            {
                Debug.LogError("Error setting up managers. Error: " + e);
            }

            ActivatePlaceholderTextForButtons();

            sceneName = SceneManager.GetActiveScene().name;

            //Ignore setting up gameplay elements if active scene is main menu.
            if (sceneName != "00_MainMenu")
            {
                SetUpGameUI();
                touchControls = GameObject.FindGameObjectWithTag("TouchController");
            }
        }

        private void Update()
        {
            //If player uses Escape, check whether pausemenu is actives. If not active, create menu and pause. If active, resume game.
            if (sceneName != "00_MainMenu")
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (!pauseMenu.activeInHierarchy)
                        ActivatePauseMenu();

                    else
                        ResumeGame();
                }
            }
        }

        void SetUpGameUI()
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
                Debug.LogError("Error setting up gameplay elements. Error: " + e);
            }
        }

        //Finds UnFinishedButtons tags and adds listener for placeholder text.
        void ActivatePlaceholderTextForButtons()
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
                Debug.LogError("Error activating/deactiving menus and fetching buttons. Error: " + e);
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

                saveManager.UnlockNewLevel();

                ActivateMenuButtons("Button");
            }

            catch (Exception e)
            {
                Debug.LogError("Error creating victory menu for player. Error: " + e);
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
                Debug.LogError("Error starting defeat menu. Error: " + e);
            }
        }

        //Activates pause menu and pauses the game
        public void ActivatePauseMenu()
        {
            try
            {
                if (songUIText == null)
                    songUIText = GameObject.FindGameObjectWithTag("SongNameText").GetComponent<ShowSongName>();

                pauseMenu.SetActive(true);

                ActivateMenuButtons("Button");

                songUIText.DisplaySongName();

                levelManager.PauseGame();
            }

            catch (Exception e)
            {
                Debug.LogError("Error pausing game. Error: " + e);
            }
        }

        //Resumes the game by removing menus and unpausing the game
        public void ResumeGame()
        {
            try
            {
                if (songUIText == null)
                    songUIText = GameObject.FindGameObjectWithTag("SongNameText").GetComponent<ShowSongName>();


                pauseMenu.SetActive(false);

                songUIText.HideSongName();

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
            if (informationObject == null)
            {
                Debug.LogError("Cannot show information text. Text or object is null.");
                return;
            }

            informationText = informationObject.GetComponent<Text>();
            informationText.text = inputText;
            informationObject.SetActive(true);

            coroutine = FlashUIText(2.0f, informationObject);
            StartCoroutine(coroutine);
        }

        //Makes the text disappear after a while
        private IEnumerator FlashUIText(float waitTime, GameObject flashingObject)
        {
            yield return new WaitForSeconds(waitTime);
            flashingObject.SetActive(false);
        }

        //Disables touch controls, mainly used to be called when game is used on android/ios when the game ends and activates end-game screen or player opens menu.
        public void DisableTouchControl()
        {
            if (touchControls != null)
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
                Debug.LogError("Error removing a player life from UI. Error: " + e);
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
