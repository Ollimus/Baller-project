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

        //Reduces player lives by 1. If player does not have lives left, end the game.
        //Also removes one player life sprite from UI.
        public void ReduceLives()
        {
            //Since PlayerManager is persistent gameobject, null check has to be done when this is used.
            if (UImanager == null)
            {
                UImanager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

                if (UImanager == null)
                {
                    Debug.LogError("No UIManager found, cannot reduce player lives.");
                    return;
                }                    
            }
            if (UImanager.playerLifeSpriteList.Count > 0)
                UImanager.RemovePlayerLifeSprite();

            if (UImanager.playerLifeSpriteList.Count == 0)
                UImanager.ActivateDefeatScreen();
        }
    }
}
