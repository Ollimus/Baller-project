using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public static GameTimer instance;

	private TextMesh gameTimer;

	private float gameTime;
	private float timeMinutes;
    [Range(0, 60)]
    private float timeSeconds;

	private bool isTimerRunning;

    private void Awake()
    {
        instance = this;
    }

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

    public float CompletionTime()
    {
        //Since value will be converted into float. Example: 1 minute 16 seconds will be 1.16.
        //But if the value is below 10 like 8 seconds, then it would be incorrectly converted into 0.8.
        if ((int)timeSeconds < 10 && (int)timeSeconds > 0)
            timeSeconds = timeSeconds / 10;

        float completionTimeFloat = float.Parse(string.Concat((int)timeMinutes, ".", (int)timeSeconds));

        return completionTimeFloat;
    }

    public string CompletionTimeText()
    {
        var gameTime = timeMinutes + " minutes " + (int)timeSeconds + " seconds.";
        return gameTime;
    }
}
