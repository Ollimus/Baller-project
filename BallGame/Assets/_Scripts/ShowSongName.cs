using UnityEngine;
using TMPro;
using System;

public class ShowSongName : MonoBehaviour
{
    TextMeshProUGUI songDisplayText;   //song display is only fetched at the start of audiomanager instance, which means that we need to feed the current songUI spot for method.
    AudioManager audioManager;

    private void Start()
    {
        try
        {
            songDisplayText = gameObject.GetComponent<TextMeshProUGUI>();
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

            audioManager.ShowSongNameInNewScene(songDisplayText);
        }

        catch (Exception e)
        {
            Debug.LogError("Error setting up required variables for ShowSongName. Error: " + e);
        }
    }
}
