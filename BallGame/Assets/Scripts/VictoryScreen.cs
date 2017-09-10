using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour {

	public Image img;

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("Disabled");
		img.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.anyKey)
		img.enabled = true;
	}
}
