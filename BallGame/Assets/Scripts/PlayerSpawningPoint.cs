using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSpawningPoint : MonoBehaviour
{
	public Transform player;
	public Transform checkpointLocation;

    int lastAddedObject;

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

            //If starting location does not exist, spawn at the first placed checkpoint location.
			if (startingPoint != null)
			{
				checkpointLocation = startingPoint.transform;
				checkpointLocations.Add(checkpointLocation); 
			}

            //If player and starting point do not exist, create player at latest checkpoint location
			if (playerObject == null && startingPoint != null)
			{
				doesPlayerExist = true;
				Instantiate (player, checkpointLocation.position, checkpointLocation.rotation);
			}

            //If player object does not exist, but starting point does, spawn player at the spawning point location.
			else if (playerObject == null)
			{
				doesPlayerExist = true;
				Instantiate (player, transform.position, transform.rotation);
			}

            //Otherwise player has been created with the level.
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

					lastAddedObject = checkpointLocations.Count;
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

    //Receives transform location from CheckPoint script and adds it into the list of checkpoints.
	public void AddCheckpoint(Transform location)
	{
		Debug.Log ("Adding a new checkpoint location");

		checkpointLocations.Add(location);
	}
}
