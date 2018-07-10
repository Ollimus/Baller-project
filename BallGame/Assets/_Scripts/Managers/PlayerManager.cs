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
        private PlayerData playerData;
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

        void Start()
        {
            playerData = SaveManager.Instance.Load();
        }

        public PlayerData PlayerData
        {
            get
            {
                return playerData; 
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
                EndLevel();
            {
                UImanager.ActivateDefeatScreen();
                StopAllCoroutines();
                IsPlayerRespawning = false;
            }
        }

        public void EndLevel()
        {
            if (UImanager == null)
            {
                Debug.LogError("Cannot End Game. UiManager not found.");
                return;
            }

            UImanager.ActivateDefeatScreen();
        }

        public void UnlockNewLevel()
        {
            playerData.UnlockedLevels = playerData.UnlockedLevels;

            SaveProgress();
        }

        public void SaveProgress()
        {
            SaveManager.Instance.Save(playerData);
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

    [Serializable]
    public class PlayerData
    {
        //Variables used for data saves.
        private int unlockedSkins;
        private int unlockedLevels;
        private int specialSkins;
        private int levelFinishTime;

        public int UnlockedLevels
        {
            get
            {
                return unlockedLevels;
            }

            set
            {
                unlockedLevels = UnlockNext(unlockedLevels);
            }
        }

        public PlayerData SaveData
        {
            get
            {
                return this;
            }
        }

        public PlayerData()
        {
            unlockedSkins = 1;
            unlockedLevels = 1;
        }

        private int UnlockNext(int unlockable)
        {
            unlockable++;
            return unlockable;
        }
    }
}
