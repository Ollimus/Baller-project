using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance;

        public bool resetSave;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            else
            {
                Destroy(gameObject);
                return;
            }

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

        private void Start()
        {
            if (resetSave)
            {
                File.Delete(Application.persistentDataPath + "/playerSave.dat");
            }
        }

        public void Save(PlayerData data)
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/playerSave.dat");

            bFormatter.Serialize(file, data);
            file.Close();
        }

        public PlayerData Load()
        {
            if (File.Exists(Application.persistentDataPath + "/playerSave.dat"))
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerSave.dat", FileMode.Open);

                try
                {
                    PlayerData playerData = (PlayerData)bFormatter.Deserialize(file);

                    return playerData;
                }

                catch (Exception e)
                {
                    Debug.LogWarning("File could not be read right. This might be because of the data class has been edited. Creating a new file instead. \n" + e);

                    file.Close();

                    return CreatePlayerFile();
                }
            }

            else
            {
                return CreatePlayerFile();
            }

        }

        private PlayerData CreatePlayerFile()
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerSave.dat");

            PlayerData playerFile = new PlayerData();

            bFormatter.Serialize(file, playerFile);
            file.Close();

            return playerFile;
        }
    }
}