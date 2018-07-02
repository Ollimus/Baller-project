using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;

public class PlayerSpawner : MonoBehaviour
{
    public int respawnTimer = 2;

    private PlayerManager playerManager;
    private Transform startingPoint;

    void Start()
    {

        startingPoint = transform;

        playerManager = PlayerManager.PlayerDataInstance;

        //Creates starting point as the first spawnable location.
        if (startingPoint != null)
            playerManager.checkpointLocations.Add(startingPoint);
        else
        {
            Debug.LogError("No starting location.");
            return;
        }
    }

    /*
     *Spawn player at starting point and check whether Player prefab has been attached.
    */
    void SpawnPlayer()
    {
        playerManager.StartPlayerRespawn(startingPoint);
    }

    //Receives transform location from CheckPoint script and adds it into the list of checkpoints.
    public void AddCheckpoint(Transform location)
    {
        playerManager.checkpointLocations.Add(location);
    }
}
