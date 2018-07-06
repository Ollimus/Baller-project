using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager PlayerDataInstance;

        public GameObject player;
        public int respawnTimer = 4;
        public List<Transform> checkpointLocations = new List<Transform>();

        private UIManager UImanager;
        private bool IsPlayerRespawning = false;

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

        //IF respawning coroutine is attached to deactiving object, the coroutine execution will stop. 
        //That's respawning coroutine is called from here, a static instance, instead.
        public void StartPlayerAtLatestCheckpoint()
        {
            if (IsPlayerRespawning)
                return;

            Transform checkpointLocation = checkpointLocations[checkpointLocations.Count - 1];

            StartCoroutine(SpawnPlayerAtCheckpoint(checkpointLocation));
        }

        public void StartPlayerRespawn(Transform spawningLocation)
        {
            if (IsPlayerRespawning)
                return;

            StartCoroutine(SpawnPlayerAtCheckpoint(spawningLocation));
        }

        private IEnumerator SpawnPlayerAtCheckpoint(Transform spawningLocation)
        {
            IsPlayerRespawning = true;

            yield return new WaitForSecondsRealtime(respawnTimer);

            if (spawningLocation != null)
            {
                Instantiate(player, spawningLocation.position, spawningLocation.rotation);
                IsPlayerRespawning = false;
            }
        }
    }
}
