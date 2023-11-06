using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class ChangeSceneScript : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private UnityEngine.SceneManagement.Scene Test;

    void Start()
    {
        if (!Application.CanStreamedLevelBeLoaded(Test.buildIndex))
        {
            Debug.LogWarning(Test.name + " can't be found in the buildsettings");
        }
    }

    public void ChangeMainSceneName(string newSceneName)
    {
        //mainSceneName = newSceneName;

        if (!Application.CanStreamedLevelBeLoaded(newSceneName))
        {
            Debug.LogWarning(newSceneName + " can't be found in the buildsettings");
        }
    }

     public void LoadScene()
    {
        //SceneManager.LoadScene(mainSceneName);
    }

    static public void LoadSceneByName(string sceneName)
    {
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogWarning(sceneName + " can't be found in the buildsettings");
        }

        SceneManager.LoadScene(sceneName);
    }

    static public void LoadNextScene() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex + 1);
    }
    public void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex + 1);
    }

    public static void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

}
