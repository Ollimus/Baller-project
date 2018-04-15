using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private UIManager UImanager;
        private int playerLives;

        public bool removeSaves = false;

        private int unlockedSkins = 1;
        public int unlockedLevels = 1;

        private void Awake()
        {
            unlockedLevels = PlayerPrefs.GetInt("Levels", 1);
        }

        //Set player lives to 5 at start of every screen.
        void Start()
        {
            playerLives = 3;

            UImanager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        }

        //Reduces player lives by 1. If player does not have lives left, end the game.
        //Also removes one player life sprite from UI.
        public void ReduceLives()
        {
            try
            {
                playerLives -= 1;

                UImanager.RemovePlayerLifeSprite();

                if (playerLives == 0)
                    UImanager.ActivateDefeatScreen();
            }

            catch (Exception e)
            {
                Debug.Log("Error with reducing lives. Error: " + e);
            }
        }

        public void SavePlayerData()
        {
            unlockedLevels++;
            PlayerPrefs.SetInt("Levels", unlockedLevels);

            Debug.Log("After save: " + unlockedLevels);
        }
    }
}
