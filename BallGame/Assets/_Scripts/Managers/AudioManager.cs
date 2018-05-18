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
    public Sound song;

    private List<Sound> playedOSTs = new List<Sound>();
    private string activeSongName;
    private ShowSongName songDisplayObject;
    private AudioSource OSTSource;

    private void Awake()
    {
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
    }

    void Start()
    {
        OSTSource = GetComponent<AudioSource>();

        foreach (Sound sound in sounds)
        {
            // Every sound tagged as OST gets added to OST array to be played by music player.
            if (sound.soundtrack)
            {
                OSTList.Add(sound);
            }
            
            else
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;

                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
            }            
        }

        if (OSTList.Count == 0)
        {
            Debug.LogError("No soundtracks set, cannot start playing soundtracks. Is this intended?");
        }
        
        else
            StartCoroutine(PlayRandomOST());
    }

    /*
     *Recursive function that constantly plays music. 
    */
    IEnumerator PlayRandomOST()
    {
        if (songDisplayObject == null)
            songDisplayObject = GameObject.FindGameObjectWithTag("SongNameText").GetComponent<ShowSongName>();

        //After the OST is extinguished, replace it with the songs that have been played.
        if (OSTList.Count == 0)
        {
            OSTList = playedOSTs;
        }

        int randomSongIndex = UnityEngine.Random.Range(0, OSTList.Count);

        song = OSTList[randomSongIndex];
        SetOSTAudioSource(song);

        //activeSongName = song.name;
        //Play(activeSongName);

        StartCoroutine(songDisplayObject.ShowCurrentSong(song));

        //Adds played song into played songs list and then removes it, so it does not play again until all songs have been gone through
        playedOSTs.Add(OSTList[randomSongIndex]);
        OSTList.RemoveAt(randomSongIndex);

        yield return new WaitForSeconds(song.clip.length);
        StartCoroutine(PlayRandomOST());
    }

    void SetOSTAudioSource(Sound ost)
    {
        OSTSource.clip = ost.clip;
        OSTSource.volume = ost.volume;
        OSTSource.pitch = ost.pitch;
        OSTSource.loop = ost.loop;

        OSTSource.Play();
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
            if (sound.source != null)
                sound.source.volume = 0f;
        }

        OSTSource.volume = 0f;
    }

    public void UnmuteAudio()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.source != null)
                sound.source.volume = 0f;
        }

        OSTSource.volume = 0f;
    }
}