using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool muteAudio;
    public List<Sound> OSTList = new List<Sound>();
    private List<Sound> playedOSTs = new List<Sound>();
    private string activeSongName;
    private Sound song;

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
       StartCoroutine(PlayRandomOST());
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

        Debug.Log("Playing song: " + activeSongName + " - " + song.clip.name);
        Play(activeSongName);

        //Adds played song into played songs list and then removes it, so it does not play again until all songs have been gone through
        playedOSTs.Add(OSTList[randomSongIndex]);
        OSTList.RemoveAt(randomSongIndex);

        yield return new WaitForSeconds(song.clip.length);
        StartCoroutine(PlayRandomOST());
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