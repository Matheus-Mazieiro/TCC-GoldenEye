using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;

    public PlayerData(Transform player)
    {
        position = new float[3];
        position[0] = player.position.x;
        position[1] = player.position.y;
        position[2] = player.position.z;
    }

}
