using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScript : Singleton<LoaderScript>
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadResourcesAsync(string resourcePath)
    {
        StartCoroutine(LoadAsync(resourcePath));
    }

    IEnumerator LoadAsync(string resourcePath)
    {
        Resources.UnloadUnusedAssets();

        ResourceRequest request = Resources.LoadAsync(resourcePath);

        while (!request.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        Instantiate(request.asset);
    }
}
