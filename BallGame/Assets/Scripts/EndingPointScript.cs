using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPointScript : MonoBehaviour
{
	private GameObject timeObject;
    private UIManager menu;
	public GameTimer timer;

	void Start()
	{
		timeObject = GameObject.Find ("GameTime");
		timer = timeObject.GetComponent<GameTimer>();

        menu = GameObject.Find("UIManager").GetComponent<UIManager>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		timer.stopTimer();
        menu.ActivateMenu();
	}
}