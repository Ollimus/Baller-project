using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    //public AudioClip music;

    // Use this for initialization
    void Start ()
    {
        musicSource.Play();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
