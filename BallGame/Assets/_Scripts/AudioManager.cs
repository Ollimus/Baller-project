using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool muteAudio;
    public List<Sound> OSTList = new List<Sound>();
    private List<Sound> playedOSTs = new List<Sound>();
    private string activeSongName;
    private Sound song;
    private float textFadeTime = 10f;

    private TextMeshProUGUI songUIText;

    public static AudioManager AudioInstance;

    private void Awake()
    {
        if (AudioInstance == null)
            AudioInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (gameObject.transform.parent != null)
        {
            gameObject.transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            return;
        }

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            if (sound.isOST)
                OSTList.Add(sound);
        }        
    }

    void Start()
    {
        songUIText = GameObject.Find("SongNameText").GetComponent<TextMeshProUGUI>();

        if (songUIText == null)
        {
            Debug.LogError("Scene has no place for Song Name text!");
        }

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
        //After the OST is extinguished, replace it with the full list.
        if (OSTList.Count == 0)
        {
            Debug.Log("Shuffleing list.");
            OSTList = playedOSTs;
        }

        int randomSongIndex = UnityEngine.Random.Range(0, (OSTList.Count) - 1);

        song = OSTList[randomSongIndex];
        activeSongName = song.name;

        Play(activeSongName);

        StartCoroutine(ShowCurrentSong(song));

        //Adds played song into played songs list and then removes it, so it does not play again until all songs have been gone through
        playedOSTs.Add(OSTList[randomSongIndex]);
        OSTList.RemoveAt(randomSongIndex);

        yield return new WaitForSeconds(song.clip.length);
        StartCoroutine(PlayRandomOST());
    }

    IEnumerator ShowCurrentSong(Sound song)
    {
        songUIText.alpha = 1;

        songUIText.text = "Song: \n" + song.clip.name;

        yield return new WaitForSeconds(textFadeTime);

        while (songUIText.alpha < 0)
        {
            Debug.Log(songUIText.alpha);
            songUIText.alpha -= (0.01f * Time.deltaTime);
        }
    }

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