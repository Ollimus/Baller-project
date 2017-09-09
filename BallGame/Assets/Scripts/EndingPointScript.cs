using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPointScript : MonoBehaviour
{
	public TextMesh victoryText;
	private GameObject timeObject;
	public GameTimer timer;

	void Start()
	{
		victoryText = GameObject.Find ("VictoryPopUp").GetComponent<TextMesh>();
		timeObject = GameObject.Find ("GameTime");
		timer = timeObject.GetComponent<GameTimer>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		timer.stopTimer();
		victoryText.text = "You won!";
	}
}