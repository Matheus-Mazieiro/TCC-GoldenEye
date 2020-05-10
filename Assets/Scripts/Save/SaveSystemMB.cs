using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystemMB : MonoBehaviour
{
    public void SavePlayer(Transform player)
    {
        SaveSystem.SavePlayer(player);
    }

    public PlayerData LoadPlayer()
    {
        return SaveSystem.LoadPlayer();
    }

    public void DeleteSavedGame()
    {
        SaveSystem.DeleteSavedGame();
    }

}
