using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioInstance;

    public Sound[] sounds;
    public bool muteAudio;
    public List<Sound> OSTList = new List<Sound>();

    private List<Sound> playedOSTs = new List<Sound>();
    private string activeSongName;
    private Sound song;
    private float textFadeTime = 10f;
    private TextMeshProUGUI songDisplayText;

    private void Awake()
    {
        songDisplayText = GameObject.Find("SongNameText").GetComponent<TextMeshProUGUI>();

        if (songDisplayText == null)
        {
            Debug.LogError("Scene has no place for Song Name text!");
        }

        /*
         * Makes sure only one instance of AudioManager is present
        */
        if (AudioInstance == null)
            AudioInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        /*
         * Makes sure audiomanager is not a child of anything, because a child cannot be indestructable.
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

        /*
         * Set up every sound attached to the sound array as audiosource-
        */
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            // Every sound tagged as OST gets added to OST array to be played by music player.
            if (sound.isOST)
                OSTList.Add(sound);
        }        
    }

    void Start()
    {
        try
        {
            StartCoroutine(PlayRandomOST());
        }

        catch (Exception e)
        {
            Debug.LogError("Error playing random OSTs. Error: " +e);
        }
    }

    /*
     *Recursive function that constantly plays music. 
    */
    IEnumerator PlayRandomOST()
    {
        //After the OST is extinguished, replace it with the songs that have been played.
        if (OSTList.Count == 0)
        {
            Debug.Log("Shuffleing list.");
            OSTList = playedOSTs;
        }

        int randomSongIndex = UnityEngine.Random.Range(0, OSTList.Count);

        song = OSTList[randomSongIndex];
        activeSongName = song.name;

        Play(activeSongName);

        StartCoroutine(ShowCurrentSong(song, songDisplayText));

        //Adds played song into played songs list and then removes it, so it does not play again until all songs have been gone through
        playedOSTs.Add(OSTList[randomSongIndex]);
        OSTList.RemoveAt(randomSongIndex);

        yield return new WaitForSeconds(song.clip.length);
        StartCoroutine(PlayRandomOST());
    }

    IEnumerator ShowCurrentSong(Sound song, TextMeshProUGUI displayText)
    {
        displayText.alpha = 1;

        displayText.text = "Song: \n" + song.clip.name;

        yield return new WaitForSeconds(textFadeTime);

        displayText.alpha = 0;
    }

    //Public method to be used by TextMesh text that displays song name in UI.
    public void ShowSongNameInNewScene(TextMeshProUGUI displayText)
    {
        StartCoroutine(ShowCurrentSong(song, displayText));
    }

    /*
     * Finds and plays sounds based on the _SONG NAME_.
    */
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + name);
            return;
        }

        s.source.Play();
    }

    public void MuteAudio()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.volume = 0f;
        }
    }

    public void UnmuteAudio()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.volume = sound.volume;
        }
    }
}