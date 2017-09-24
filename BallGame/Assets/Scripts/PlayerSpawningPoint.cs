using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSpawningPoint : MonoBehaviour
{
	public Transform player;
	public Transform checkpointLocation;

	private GameObject startingPoint;
	private GameObject playerObject;
	public List<Transform> checkpointLocations = new List<Transform>();

	public bool doesPlayerExist = false;
	public float playerDeathTime;
	public int respawnTimer = 2;

	void Start()
	{
		try
		{
			playerObject = GameObject.FindGameObjectWithTag("Player");
			startingPoint = GameObject.FindGameObjectWithTag("StartingPoint");

			if (startingPoint != null)
			{
				checkpointLocation = startingPoint.transform;
				checkpointLocations.Add(checkpointLocation); 
			}

			if (playerObject == null && startingPoint != null)
			{
				doesPlayerExist = true;
				Instantiate (player, checkpointLocation.position, checkpointLocation.rotation);
			}

			else if (playerObject == null)
			{
				doesPlayerExist = true;
				Instantiate (player, transform.position, transform.rotation);
			}

			else
			{
				doesPlayerExist = true;
			}
		}

		catch (Exception e)
		{
			Debug.Log ("Error with Spawning Start function: " + e);
		}
	}

	void Update ()
	{
		//Searches for Player -object and if one does not exist, sets player existance false.
		playerObject = GameObject.FindGameObjectWithTag("Player");

		if (playerObject == null)
        {
			doesPlayerExist = false;
		}

		if (doesPlayerExist == false)
		{
			/*
			 *After respawn time has passed, spawn player at the latest unlocked checkpoint. 
			*/
			try
			{
				if (Time.time >= playerDeathTime)
				{
					Debug.Log ("Spawning Player");

					doesPlayerExist = true;

					var lastAddedObject = checkpointLocations.Count;
					lastAddedObject -= 1;

					checkpointLocation = checkpointLocations[lastAddedObject];
					Instantiate(player, checkpointLocation.position, checkpointLocation.rotation);
				}
			}

			catch (Exception e)
			{
				Debug.Log ("Error spawning the player. Error: " + e);	
			}
		}
	}

	public void addCheckpoint(Transform location)
	{
		Debug.Log ("Adding a new checkpoint location");

		checkpointLocations.Add(location);
	}
}
