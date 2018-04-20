using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager PlayerDataInstance;

        private UIManager UImanager;
        private int playerLives;

        private void Awake()
        {


            /*
             * Makes sure only one instance of PlayerDataManager is present
            */
            if (PlayerDataInstance == null)
                PlayerDataInstance = this;

            else
            {
                Destroy(gameObject);
                return;
            }

            UImanager = gameObject.transform.parent.GetComponentInChildren<UIManager>();

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

        //Set player lives to 5 at start of every screen.
        void Start()
        {
            playerLives = 3;
        }

        //Reduces player lives by 1. If player does not have lives left, end the game.
        //Also removes one player life sprite from UI.
        public void ReduceLives()
        {
            try
            {
                if (UImanager == null)
                    UImanager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

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


    }
}
