using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class PlayerCustomization : MonoBehaviour
{

    public static PlayerCustomization instance;

    public Dictionary<string, Sprite> availableSkins = new Dictionary<string, Sprite>();    // All skins in the game.
    public PlayerSkins[] playerSkins;

    private Dictionary<string, Sprite> unlockedSkins = new Dictionary<string, Sprite>();    //Skins available to player.

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        foreach (PlayerSkins playerSkin in playerSkins)
        {
            if (playerSkin != null)
            {
                if (!availableSkins.ContainsKey(playerSkin.key))
                    availableSkins.Add(playerSkin.key, playerSkin.skin);
            }
        }

        FetchPlayerSkins();
    }

    public void ChooseSkin(string name)
    {
        if (!unlockedSkins.ContainsKey(name))
        {
            UIManager.instance.ShowInformationText("You have not unlocked the skin!");
            return;
        }

        Sprite skin = availableSkins[name];

        if (skin == null)
        {
            Debug.LogError("Selected skin was not found.");
            return;
        }

        else
        {
            PlayerManager.PlayerDataInstance.ChangePlayerSkin(skin);
            UIManager.instance.ShowInformationText("Succesfully changed.");
        }
        
    }

    private void FetchPlayerSkins()
    {
        List<string> playerUnlockedSkins = PlayerManager.PlayerDataInstance.PlayerData.UnlockedSkinKeys;

        for (int i = 0; i < playerUnlockedSkins.Count; i++)
        {
            if (availableSkins.ContainsKey(playerUnlockedSkins[i]))
            {
                unlockedSkins.Add(playerUnlockedSkins[i], availableSkins[playerUnlockedSkins[i]]);
            }
        }
    }
}
