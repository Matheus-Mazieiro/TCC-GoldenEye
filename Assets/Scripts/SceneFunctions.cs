using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SceneFunctions : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
            Cursor.visible = true;
        if(Camera.main.GetComponent<CinemachineBrain>())
        {
            if(Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera != null)
                Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Priority = 9;
            GameObject.Find("Camera").GetComponent<CinemachineVirtualCamera>().Priority = 10;
        }
    }
    public void GoToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
