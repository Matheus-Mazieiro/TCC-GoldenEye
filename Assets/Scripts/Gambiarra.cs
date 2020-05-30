using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gambiarra : MonoBehaviour
{
    public Movement player;

    public void EnableMov(string state)
    {
        if (state == "false")
        {
            player.myCC.enabled = false;
            player.enabled = false;
        }
        else if (state == "true")
        {
            player.myCC.enabled = true;
            player.enabled = true;
        }
    }
}
