using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unparenting : MonoBehaviour
{
    public Movement player;

    public void SetParentNull(Transform target)
    {
        target.parent = null;
    }

    public void Gambiarra(string target)
    {
        if (target == "false")
        {
            player.myCC.enabled = false;
            player.enabled = false;
        }else if(target == "true")
        {
            player.myCC.enabled = true;
            player.enabled = true;
        }
    }
}
