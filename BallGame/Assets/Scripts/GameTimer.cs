using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
	public TextMesh gameTimer;

	private float gameTime;
	private float timeMinutes;
	private float timeSeconds;

	private bool isTimerRunning;

    void Start ()
	{
		isTimerRunning = true;
		
		gameTimer = gameObject.GetComponent<TextMesh>();
		timeSeconds = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateTimer ();
	}

	private void UpdateTimer()
	{
		if (isTimerRunning)
		{
            timeSeconds += Time.deltaTime;

			if (timeSeconds >= 60)
			{
				timeMinutes++;
				timeSeconds = 0;
			}

			gameTimer.text = "Time: " + timeMinutes +  "m "  + (int)timeSeconds + "s";
		}
	}

	public void StopTimer()
	{
		isTimerRunning = false;
	}

    public string EndingTime()
    {
        var gameTime = timeMinutes + " minutes " + (int)timeSeconds + " seconds.";
        return gameTime;
    }
}
