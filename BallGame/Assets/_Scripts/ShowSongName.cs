using UnityEngine;
using TMPro;
using System;
using Managers;
using System.Collections;

public class ShowSongName : MonoBehaviour
{
    TextMeshProUGUI songDisplayText;   //song display is only fetched at the start of audiomanager instance, which means that we need to feed the current songUI spot for method.
    AudioManager audioManager;
    UIManager uiManager;

    private float txtFadeTime = 5f;

    private void Start()
    {
        try
        {
            songDisplayText = gameObject.GetComponent<TextMeshProUGUI>();

            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

        catch (Exception e)
        {
            Debug.LogError("Error setting up required variables for ShowSongName. Error: " + e);
        }

        if (audioManager.song != null)
        {
            StartCoroutine(ShowCurrentSong(audioManager.song));
        }


    }

    public IEnumerator ShowCurrentSong(Sound song)
    {
        songDisplayText = this.GetComponent<TextMeshProUGUI>();

        songDisplayText.alpha = 1;

        songDisplayText.text = "Song: \n" + song.clip.name;

        yield return new WaitForSeconds(txtFadeTime);

        songDisplayText.alpha = 0;
    }

    public void ShowSongNameInPause()
    {
        songDisplayText.alpha = 1;
    }

    public void HideSongName()
    {
        songDisplayText.alpha = 0;
    }

    public void FetchTextDetails()
    {

    }

}
