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
            string filePath = Application.streamingAssetsPath;

            if (debugMobileTutorial)
            {
                ReadJsonFileData(filePath, mobileFileName, null);
                return;
            }

            else
            {
                ReadJsonFileData(filePath, pcFileName, null);
                return;
            }
        }

        RuntimePlatform playerPlatform = Application.platform;

        if (playerPlatform == RuntimePlatform.WindowsPlayer || playerPlatform == RuntimePlatform.OSXPlayer || playerPlatform == RuntimePlatform.LinuxPlayer)
        {
            string filePath = Application.streamingAssetsPath;

            ReadJsonFileData(filePath , pcFileName, null);
        }

        else if (playerPlatform == RuntimePlatform.Android)
        {
            string filePath = "jar:file://" + Application.dataPath + "/assets/";

            string extractedJson = ExtractedJsonFile(filePath, mobileFileName);

            ReadJsonFileData(filePath, mobileFileName, extractedJson);
        }

        else if (playerPlatform == RuntimePlatform.IPhonePlayer)
        {
            string filePath = Application.dataPath + "/Raw";
            ReadJsonFileData(filePath, mobileFileName, null);
        }
	}

    private string ExtractedJsonFile(string filePath, string fileName)
    {
        string newPath = Path.Combine(filePath, fileName);

        WWW reader = new WWW(newPath);

        while (!reader.isDone) { }
        
        return reader.text;
    }


    private void ReadJsonFileData (string filePath, string fileName, string extracedJson)
    {
        tutorialDict = new Dictionary<string, string>();
        filePath = Path.Combine(filePath, fileName);

        if (extracedJson != null)
        {
            CreateDictionary(extracedJson);
            return;
        }

        if (File.Exists(filePath))
        {
            string jsonDataFile = File.ReadAllText(filePath);

            CreateDictionary(jsonDataFile);
        }

        else
        {
            Debug.LogError("Cannot find file!");
        }
    }

    private void CreateDictionary(string dataAsJson)
    {
        TextData loadedData = JsonUtility.FromJson<TextData>(dataAsJson);

        for (int i = 0; i < loadedData.tutorialtexts.Length; i++)
        {
            tutorialDict.Add(loadedData.tutorialtexts[i].key, loadedData.tutorialtexts[i].value);
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