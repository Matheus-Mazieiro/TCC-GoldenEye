using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Unparenting : MonoBehaviour
{
    public Movement player;
    public CinemachineVirtualCamera candelabroCam;

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

    public void CairCandelabroMethod()
    {
        StartCoroutine(CairCandelabro());
    }
    public IEnumerator CairCandelabro()
    {
        candelabroCam.Priority = 11;
        yield return new WaitForSeconds(1.5f);
        candelabroCam.Priority = 9;
    }
}
