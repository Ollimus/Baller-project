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
        audioManager = AudioManager.AudioInstance;

        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found.");
            return;
        }

        if (audioManager.song.clip != null)
            StartCoroutine(ShowCurrentSong(audioManager.song));
    }

    public IEnumerator ShowCurrentSong(Sound song)
    {
        if (songDisplayText == null)
            songDisplayText = GetComponent<TextMeshProUGUI>();

        songDisplayText.alpha = 1;
        songDisplayText.text = "Song: \n" + song.clip.name;

        yield return new WaitForSeconds(txtFadeTime);

        songDisplayText.alpha = 0;
    }

    public void DisplaySongName()
    {
        songDisplayText.alpha = 1;
    }

    public void HideSongName()
    {
        songDisplayText.alpha = 0;
    }
}
