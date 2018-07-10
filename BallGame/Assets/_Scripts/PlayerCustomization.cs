using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour {

    public PlayerCustomization instance;

    public Dictionary<string, Sprite> availableSkins;
    public PlayerSkins[] playerSkins;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (PlayerSkins playerSkin in playerSkins)
        {
            if (playerSkin != null)
            {
                availableSkins.Add(playerSkin.key, playerSkin.skin);
            }
        }
    }
}
