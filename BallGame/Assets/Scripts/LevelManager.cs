using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using Managers;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        Scene scene;
        int sceneNumber;

        /*IMPORTANT
         * -------
         * Button names for easy control of what buttons do.
         * -------
        */
        string nextLevelBtnName = "btnNextLevel";
        string retryLevelBtnName = "btnRetry";
        string mainMenuBtnName = "btnExitMainMenu";
        string resumeGameBtnName = "btnResume";


        private void Start()
        {
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

        //Makes sure game has been unpaused when loaded into another level.
        private void UnPauseGame()
        {
            Time.timeScale = 1f;
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
            try
            {
                SceneManager.LoadScene(scene.buildIndex + 1);
            }

            catch (Exception e)
            {
                Debug.Log("Error changing to the next level. Returning to main menu. Error: " + e);
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
                UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

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
