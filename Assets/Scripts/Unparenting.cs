using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unparenting : MonoBehaviour
{
    public void SetParentNull(Transform target)
    {
        target.parent = null;
    }
}
