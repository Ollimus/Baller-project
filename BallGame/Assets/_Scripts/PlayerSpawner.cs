using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player;
    public int respawnTimer = 2;

    private Transform checkpointLocation;
    private Transform startingPoint;
    private List<Transform> checkpointLocations = new List<Transform>();    //List of checkpoint locations
    int lastAddedObject;


    void Start()
    {
        startingPoint = transform;

        //Creates starting point as the first spawnable location.
        if (startingPoint != null)
            checkpointLocations.Add(startingPoint);
        else
        {
            Debug.LogError("No starting location.");
            return;
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject == null)
            SpawnPlayer();
    }

    /*
     *Spawn player at starting point and check whether Player prefab has been attached.
    */
    void SpawnPlayer()
    {
        if (player != null)
            Instantiate(player, startingPoint, startingPoint);

        else
        {
            Debug.LogError("Player prefab is not assigned to PlayerSpawner.");
            return;
        }
    }

    //Receives transform location from CheckPoint script and adds it into the list of checkpoints.
    public void AddCheckpoint(Transform location)
    {
        checkpointLocations.Add(location);
    }

    /*
    *After respawn time has passed, spawn player at the latest unlocked checkpoint. 
    */

    public IEnumerator SpawnPlayerAtCheckpoint()
    {
        yield return new WaitForSecondsRealtime(respawnTimer);

        lastAddedObject = checkpointLocations.Count -1;
        checkpointLocation = checkpointLocations[lastAddedObject];

        Instantiate(player, checkpointLocation.position, checkpointLocation.rotation);
    }
}
