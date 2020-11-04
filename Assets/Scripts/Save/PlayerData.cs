using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public int level;

    public PlayerData(Transform player)
    {
        position = new float[3];
        position[0] = player.position.x;
        position[1] = player.position.y;
        position[2] = player.position.z;
        level = SceneManager.GetActiveScene().buildIndex;
    }

}
