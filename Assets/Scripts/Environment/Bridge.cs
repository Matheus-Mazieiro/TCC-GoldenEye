using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public Transform bridgeMesh;
    public bool increase;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            /*if (increase)
                bridgeMesh.localScale = Vector3.one;
            else bridgeMesh.localScale = new Vector3(0, 1, 1);*/
            StartCoroutine(GrowBridge());
        }
    }

    IEnumerator GrowBridge()
    {
        while (true)
        {
            if (increase)
            {
                bridgeMesh.localScale = Vector3.Lerp(bridgeMesh.localScale, Vector3.one, 0.25f);
                if (bridgeMesh.localScale.x >= 0.99f)
                    break;
            }
            else
            {
                bridgeMesh.localScale = Vector3.Lerp(bridgeMesh.localScale, new Vector3(0, 1, 1), 0.005f);
                if (bridgeMesh.localScale.x <= 0.05f)
                    break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
