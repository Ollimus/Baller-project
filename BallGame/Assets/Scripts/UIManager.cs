using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject menuScreen;

    /*// Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	}*/

    public void ActivateMenu()
    {
        menuScreen.SetActive(true);
    }
}
