using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFunctions : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
            Cursor.visible = true;
    }
    public void GoToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
