using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagerObject : MonoBehaviour
{
    CinemachineVirtualCamera mainCamera;
    CinemachineVirtualCamera myCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("Camera").GetComponent<CinemachineVirtualCamera>();
        myCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ChangeCamera()
    {
        myCamera.Priority = 11;
    }

    public void RezetCamera()
    {
        foreach (CinemachineVirtualCamera item in GameObject.FindObjectsOfType<CinemachineVirtualCamera>())
        {
            item.Priority = 9;
        }
    }
}
