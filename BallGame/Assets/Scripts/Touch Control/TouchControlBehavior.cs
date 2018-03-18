using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControlBehavior : MonoBehaviour {

    public bool debuggingMode = true;

	// Use this for initialization
	void Start ()
    {
        //Checks whether scene has touchcontrols set up.
        /*if (touchControls != null)
        {
            //Receives and sets up information about user's operating system.
            operatingSystemCheck = SystemInfo.operatingSystem;

            //If user runs the game on windows/mac, disables touch controls. Otherwise activates them (for Android/IOS).
            if (operatingSystemCheck.StartsWith("Windows") || operatingSystemCheck.StartsWith("Mac"))
            {
                touchControls.SetActive(false);
            }

            else
            {
                touchControls.SetActive(true);
            }
        }*/
    }

    // Update is called once per frame
    void Update ()
    {
		
	}


}
