using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player;
    public int respawnTimer = 2;
    public PlayerManager playerManager;

    private Transform checkpointLocation;
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

        GameObject scenePlayer = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Destroy(scenePlayer);
            Debug.LogWarning("Was this intended? Player was found in scene. Player was deleted.");
        }

        SpawnPlayerInstantly();
    }

    /*
     *Spawn player at starting point and check whether Player prefab has been attached.
    */
    void SpawnPlayer()
    {
        playerManager.StartPlayerRespawn(startingPoint);
    }

    void SpawnPlayerInstantly()
    {
        playerManager.InstantlySpawnPlayer(startingPoint);
    }

    //Receives transform location from CheckPoint script and adds it into the list of checkpoints.
    public void AddCheckpoint(Transform location)
    {
        playerManager.checkpointLocations.Add(location);
    }
}
