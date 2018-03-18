using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;

public class CheckpointScript : MonoBehaviour
{
    public string checkpointText = "Checkpoint unlocked!";

    private UIManager manager;
	private Transform location;
	private GameObject spawningGameObject;
	private PlayerSpawningPoint spawningPoints;

	private bool isCheckpointActivated = false;

	void Start()
	{
		try
		{
            manager = GameObject.Find("UIManager").GetComponent<UIManager>();

			spawningGameObject = GameObject.FindGameObjectWithTag("StartingPoint");

			if (spawningGameObject != null)
			{
				spawningPoints = spawningGameObject.GetComponent<PlayerSpawningPoint>();
			}
		}

		catch (Exception e)
		{
			Debug.Log("Spawning handling gameobject not found. Error: " + e);
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

                try
                {
                    manager.ShowInformationText(checkpointText);
                }

                catch (Exception e)
                {
                    Debug.Log("Error with displaying information. Error: " + e);
                }
			}
		}

		catch (Exception e)
		{
			Debug.Log ("Checkpoint activation failed. Error: " + e);
		}
	}
}
