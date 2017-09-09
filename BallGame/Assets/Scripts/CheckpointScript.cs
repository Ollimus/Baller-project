using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckpointScript : MonoBehaviour
{	
	private Transform location;
	private string name;

	private GameObject spawningGameObject;
	private PlayerSpawningPoint spawningPoints;

	private bool isCheckpointActivated = false;

	void Start()
	{
		try
		{
			spawningGameObject = GameObject.FindGameObjectWithTag("SpawningCheckpoints");

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
				spawningPoints.addCheckpoint (location);
				isCheckpointActivated = true;
			}
		}

		catch (Exception e)
		{
			Debug.Log ("Checkpoint activation failed. Error: " + e);
		}

	}
}
