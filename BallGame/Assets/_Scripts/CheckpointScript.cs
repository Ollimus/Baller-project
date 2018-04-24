using UnityEngine;
using System;
using Managers;

public class CheckpointScript : MonoBehaviour
{
    public string checkpointText = "Checkpoint unlocked!";

    private UIManager uiManager;
	private Transform location;
	private GameObject spawningGameObject;
	private PlayerSpawningPoint spawningPoints;

	private bool isCheckpointActivated = false;

	void Start()
	{
		try
		{
            uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

			spawningPoints = GameObject.FindGameObjectWithTag("StartingPoint").GetComponent<PlayerSpawningPoint>();
		}

		catch (Exception e)
		{
			Debug.LogError("Spawning handling gameobject not found. Error: " + e);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		try
		{
			if (!isCheckpointActivated && other.CompareTag("Player"))
			{
				location = this.transform;
				spawningPoints.AddCheckpoint (location);
				isCheckpointActivated = true;

                uiManager.ShowInformationText(checkpointText);
			}
		}

		catch (Exception e)
		{
			Debug.Log ("Checkpoint activation failed. Error: " + e);
		}
	}
}
