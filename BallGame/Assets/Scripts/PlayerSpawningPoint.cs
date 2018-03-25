using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;

public class PlayerSpawningPoint : MonoBehaviour
{
	private GameObject player;
	private Transform checkpointLocation;
    private GameObject startingPoint;
	private GameObject playerObject;
	private List<Transform> checkpointLocations = new List<Transform>();    //List of checkpoint locations

    int lastAddedObject;
    public bool doesPlayerExist = false;
	public float playerDeathTime;
	public int respawnTimer = 2;

	void Start()
	{
        CreatePlayer();

        try
        {
            player = Resources.Load("Player") as GameObject;
        }

        catch (Exception e)
        {
            Debug.Log("Error setting up Player Prefab. Error: " + e);
        }
	}

    void CreatePlayer()
    {
        try
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            startingPoint = transform.gameObject;

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
                Instantiate(player, checkpointLocation.position, checkpointLocation.rotation);
            }

            //If player object does not exist, but starting point does, spawn player at the spawning point location.
            else if (playerObject == null)
            {
                doesPlayerExist = true;
                Instantiate(player, transform.position, transform.rotation);
            }

            //Otherwise player has been created with the level.
            else
            {
                doesPlayerExist = true;
            }
        }

        catch (Exception e)
        {
            Debug.Log("Error with Spawning Start function: " + e);
        }
    }

    //Searches for Player -object and if one does not exist, sets player existance false.
    void Update ()
	{
		playerObject = GameObject.FindGameObjectWithTag("Player");

		if (doesPlayerExist == false && playerObject == null)
		{
            SpawnPlayerAtCheckpoint();
		}
	}

    //Receives transform location from CheckPoint script and adds it into the list of checkpoints.
	public void AddCheckpoint(Transform location)
	{
		Debug.Log ("Adding a new checkpoint location");

		checkpointLocations.Add(location);
	}

    /*
    *After respawn time has passed, spawn player at the latest unlocked checkpoint. 
    */

    public IEnumerator SpawnPlayerAtCheckpoint()
    {
            yield return new WaitForSecondsRealtime(respawnTimer);

            Debug.Log("Spawning Player");

            doesPlayerExist = true;

            lastAddedObject = checkpointLocations.Count;
            lastAddedObject -= 1;

            checkpointLocation = checkpointLocations[lastAddedObject];
            Instantiate(player, checkpointLocation.position, checkpointLocation.rotation);        
    }

    private IEnumerator StartDeathTimer()
    {;
        yield return new WaitForSeconds(respawnTimer);
        Debug.Log(Time.time);
    }
}
