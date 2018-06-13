using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class TutorialTexts : MonoBehaviour {

    public TextItems[] texts;
    public bool debugMobileTutorial;
    public string pcFileName = "TutorialPC.json";
    public string mobileFileName = "TutorialMobile.json";

    private Dictionary<string, string> tutorialDict;

    void Awake ()
    {
        if (Application.isEditor)
        {
            if (debugMobileTutorial)
            {
                CreateDictionaryFromJSON(mobileFileName);
                return;
            }

            else
                CreateDictionaryFromJSON(pcFileName);
        }

        RuntimePlatform playerPlatform = Application.platform;

        if (playerPlatform == RuntimePlatform.WindowsPlayer || playerPlatform == RuntimePlatform.OSXPlayer || playerPlatform == RuntimePlatform.LinuxPlayer)
        {
            CreateDictionaryFromJSON(pcFileName);
        }

        else if (playerPlatform == RuntimePlatform.Android || playerPlatform == RuntimePlatform.Android)
        {
            CreateDictionaryFromJSON(mobileFileName);
        }
	}

    private void CreateDictionaryFromJSON (string fileName)
    {
        tutorialDict = new Dictionary<string, string>();

        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            TextData loadedData = JsonUtility.FromJson<TextData>(dataAsJson);

            for (int i = 0; i < loadedData.tutorialtexts.Length; i++)
            {
                tutorialDict.Add(loadedData.tutorialtexts[i].key, loadedData.tutorialtexts[i].value);
            }
        }

        else
        {
            Debug.LogError("Cannot find file!");
        }
    }

    public string GetText(string key)
    {
        string text = tutorialDict[key];

        return text;
    }
}

[Serializable]
public class TextData
{
    public TextItems[] tutorialtexts;
}

[Serializable]
public class TextItems
{
    public string key;
    public string value;
}