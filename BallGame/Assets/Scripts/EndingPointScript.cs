using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPointScript : MonoBehaviour
{
	private GameObject timeObject;
    private UIManager menu;
	public GameTimer timer;
    private string completionTime;

    //Set time and menu objects
	void Start()
	{
		timeObject = GameObject.Find ("GameTime");
		timer = timeObject.GetComponent<GameTimer>();

        menu = GameObject.Find("UIManager").GetComponent<UIManager>();
	}

    //When player touches endpoint flag, stop the timer, get the endtime and activate the menu with completion time
	void OnTriggerEnter2D(Collider2D other)
	{
        //Stops timer and takes time
        timer.StopTimer();
		completionTime = timer.EndingTime();

        //Activates the menu and sends it the time
        menu.ActivateMenu(completionTime);

        //disables touch control settings.
        menu.DisableTouchControl();

        Time.timeScale = 0f;
	}
}