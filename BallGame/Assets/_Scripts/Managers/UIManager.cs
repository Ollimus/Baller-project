using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

namespace Managers
{
    public partial class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        //Gameobjects to affected by UIManager
        private GameObject informationObject;
        private ShowSongName songUIText;
        
        //Menus
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
        private PlayerManager playerManager;

        private GameObject touchControls;

        [HideInInspector]
        public List<GameObject> playerLifeSpriteList = new List<GameObject>();

        private IEnumerator coroutine;
        private string operatingSystemCheck;
        private string sceneName;

        private Text informationText;
        private Text completionTimeText;
        public Text bestTime;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            try
            {
                levelManager = gameObject.transform.parent.GetComponentInChildren<LevelManager>();
                informationObject = GameObject.FindGameObjectWithTag("InformationText");

                if (PlayerManager.PlayerDataInstance != null)
                    playerManager = PlayerManager.PlayerDataInstance;
            }

            catch (Exception e)
            {
                Debug.LogError("Error setting up managers. Error: " + e);
            }

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
            ButtonArray = GameObject.FindGameObjectsWithTag(button);

            if (ButtonArray == null)
            {
                Debug.LogError("UIManager cannot find any button -tag buttons. Cannot activate menu buttons.");
                return;
            }

            levelManager.InitiateUIButtons(ButtonArray, this);

        }

        //Handles activation of End Menu
        public void ActivateVictoryScreen(string completionTimeString, float completionTime)
        {
            completionTimeText = victoryMenu.transform.Find("txtCompletionTime").GetComponentInChildren<Text>();
            Text bestTimeText = victoryMenu.transform.Find("txtBestTime").GetComponentInChildren<Text>();

            if (completionTimeString == null || bestTimeText == null)
            {
                Debug.LogError("No completion or best time gameobjects found in UIManager.");
                return;
            }

            float bestTime = float.Parse(playerManager.LoadBestimeOfActiveScene(sceneName, false));

            if ((completionTime < bestTime  || bestTime > 0 && completionTime > 0) | bestTime == 0)
            {
                bestTimeText.text = "New Record: \n" + completionTimeString;

                bestTimeText.color = Color.magenta;

                playerManager.SaveLevelCompletionTime(sceneName, completionTime);
            }

            else if (completionTime > bestTime && bestTime > 0)
            {
                bestTimeText.text = playerManager.LoadBestimeOfActiveScene(sceneName, true);
            }

            completionTimeText.text = completionTimeString;

            victoryMenu.SetActive(true);

            playerManager.UnlockNewLevel();

            ActivateMenuButtons("Button");
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
