using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

namespace Managers
{
    public partial class LevelManager : MonoBehaviour
    {
        Scene scene;
        int sceneNumber;
        UIManager uiManager;

        /*IMPORTANT
         * -------
         * Button names for easy control of what buttons do.
         * -------
        */
        string nextLevelBtnName = "btnNextLevel";
        string retryLevelBtnName = "btnRetry";
        string mainMenuBtnName = "btnExitMainMenu";
        string resumeGameBtnName = "btnResume";

        /*private void Awake()
        {
            string test = "btnLevel13232";

            for (int i = 0; i < test.Length; i++)
            {
                if (Char.IsDigit(test[i]))
                    Debug.Log(test[i]);
            }
        }*/

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "00_MainMenu")
                ActivateMainMenuLevels();
        }

        private void Start()
        {
            uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

            if (uiManager == null)
                Debug.LogWarning("UImanager not found. ");            

            scene = SceneManager.GetActiveScene();
            UnPauseGame();
        }

        public void InitiateButtons(GameObject[] buttonArray)
        {
            try
            {
                foreach (GameObject buttons in buttonArray)
                {
                    Button button = buttons.GetComponent<Button>();

                    string btnName = button.name;

                    if (btnName == nextLevelBtnName)
                    {
                        button.onClick.AddListener(() => NextLevel());
                    }

                    if (btnName == retryLevelBtnName)
                    {
                        button.onClick.AddListener(() => RetryLevel());
                    }

                    if (btnName == mainMenuBtnName)
                    {
                        button.onClick.AddListener(() => ChangeScene("00_MainMenu"));
                    }

                    if (btnName == resumeGameBtnName)
                    {
                        button.onClick.AddListener(() => ResumeGame());
                    }
                }
            }

            catch (Exception e)
            {
                Debug.Log("Error setting up buttons. Error: " + e);
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
        }

        //Makes sure game has been unpaused when loaded into another level.
        public void UnPauseGame()
        {
            Time.timeScale = 1f;
        }


        private void ActivateMainMenuLevels()
        {
            GameObject[] ButtonArray = GameObject.FindGameObjectsWithTag("LevelButton");

            foreach (GameObject gameobject in ButtonArray)
            {
                Button button = gameobject.GetComponent<Button>();

                string name = button.name;

                for (int i = 0; i < name.Length; i++)
                {
                    if (Char.IsDigit(name[i]))
                    {
                        string levelName = "01_Level" + name[i];

                        button.onClick.AddListener(() => ChangeScene(levelName));
                    }
                }
            }
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
            if (Application.CanStreamedLevelBeLoaded(scene.buildIndex + 1))
            {
                SceneManager.LoadScene(scene.buildIndex + 1);
            }

            else
            {
                Debug.Log("Error changing to the next level. Returning to main menu.");
                ChangeScene("00_MainMenu");
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

        public void ResumeGame()
        {
            try
            {
                uiManager.ResumeGame();
            }

            catch (Exception e)
            {
                Debug.Log("Cannot Resume Game. Error: " + e);
            }
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}
