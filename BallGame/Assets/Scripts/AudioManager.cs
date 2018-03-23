using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool muteAudio;
    public List<Sound> themeNames = new List<Sound>();
    private AudioSource activeSong;

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
                themeNames.Add(sound);
        }        
    }

    void Start()
    {
       StartCoroutine(PlayRandomOST());
    }

    IEnumerator PlayRandomOST()
    {
        Sound song = themeNames[UnityEngine.Random.Range(0, themeNames.Count)];

        string songName = song.name;

        Debug.Log("Playing song: " + songName + " - " + song.clip.name);

        Play(songName);

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
