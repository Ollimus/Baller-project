using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool muteAudio;

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
        }        
    }

    void Start()
    {
        Play("Theme");
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
