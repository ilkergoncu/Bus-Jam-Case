using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class SceneLoader : MonoBehaviour
{
    //<summary>
    //This class holds the functions for loading the next scene, restarting the current scene, and returning to the start screen.
    //These functions are called OnClick.
    //<summary>

    public TextMeshProUGUI levelNumberText; 

    void Awake()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateLevelNumber();
    }

    private void UpdateLevelNumber()
    {
       
        int levelNumber = SceneManager.GetActiveScene().buildIndex + 1;
        levelNumberText.text = "Level " + (levelNumber - 1); 
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        GameObject.Find("SaveManager").GetComponent<ManagerSave>().SaveSceneIndexOnQuit();
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            UpdateLevelNumber(); 
        }
        else
        {
            Debug.Log("All scenes are completed");
            SceneManager.LoadScene(1);
        }
    }

    public void ReturnToStartScreen()
    {
        SceneManager.LoadScene(0);
    }




    public void ReloadScene()
    {
       
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }


void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }
}

