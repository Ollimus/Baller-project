using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager PlayerDataInstance;
        private Sprite activePlayerSkin;

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

        public void ChangePlayerSkin(Sprite skin)
        {
            SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();

            playerRenderer.sprite = skin;
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
            {
                EndLevel();
            }
        }

        private void EndLevel()
        {
            UImanager.ActivateDefeatScreen();
            StopAllCoroutines();
            IsPlayerRespawning = false;
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

        /*
        * Instantly spawns player at starting location. Coroutine is delayed respawn, which isn't suitable for scene start.
        */
        public void InstantlySpawnPlayer(Transform spawningLocation)
        {
            Instantiate(player, spawningLocation.position, spawningLocation.rotation);
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
        private int unlockedLevels;
        private int levelFinishTime;
        private List<string> unlockedSkinKeys = new List<string>();

        public int UnlockedLevels
        {
            get
            {
                return unlockedLevels;
            }

            set
            {
                unlockedLevels = UnlockNextLevel(unlockedLevels);
            }
        }

        public PlayerData SaveData
        {
            get { return this; }
        }

        public List<string> UnlockedSkinKeys
        {
            get { return unlockedSkinKeys; }
        }

        public PlayerData()
        {
            unlockedLevels = 1;
            UnlockedSkinKeys.Add("Defaul");
            UnlockedSkinKeys.Add("BetaReward");
        }

        private int UnlockNextLevel(int unlockable)
        {
            unlockable++;
            return unlockable;
        }

        /*
         * Checks whether skin key exists within available skins and then checks whether the skin has already been added to player skins.
         * 
        */
        public void AddSkinKey(string key, Dictionary<string, Sprite> skinDictionary)
        {
            if (!skinDictionary.ContainsKey(key))
            {
                if (!UnlockedSkinKeys.Contains(key))
                {
                    UnlockedSkinKeys.Add(key);
                }

                else
                    return;
            }
        }
    }
}
