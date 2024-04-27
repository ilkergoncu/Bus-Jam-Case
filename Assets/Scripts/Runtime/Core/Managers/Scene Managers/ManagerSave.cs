using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ManagerSave : MonoBehaviour
{
    //<summary>
    //This class is a basic save manager for the scenes. It saves the last scene when quit and loads the left off scene at start.
    //<summary>

    [SerializeField]
     private TMP_Text lastLevelText; // a level text for the game start screen. 


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        if (PlayerPrefs.GetInt("hasGameStartedBefore", 0) == 0) // Checks if the game has been started before to get scene index.
        {
            lastLevelText.text = ("LEVEL 1").ToString(); 
        }
        else
        {
            int lastSceneIndex = PlayerPrefs.GetInt("lastSceneIndex", 1);
            lastLevelText.text = "LEVEL " + (lastSceneIndex).ToString();
        }


       
      
    }

    //<summary>
    //This function loads the appropriate scene and checks if its the first time player opens the game.
    //Gets called in a OnClick in the start screen.
    //<summary>


    public void LoadAppropriateScene()
         {
      
        if (PlayerPrefs.GetInt("hasGameStartedBefore", 0) == 0)
        {
  
            PlayerPrefs.SetInt("hasGameStartedBefore", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            
            int lastSceneIndex = PlayerPrefs.GetInt("lastSceneIndex", 1);

            if (lastSceneIndex > 0 && lastSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(lastSceneIndex);
               
            }
            else
            {
               
                SceneManager.LoadScene(1);
             
            }
        }
    }

    //Saves the last scene on quit with a PlayerPrefs

    public void SaveSceneIndexOnQuit()
    {
    
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex > 0) 
        {
            PlayerPrefs.SetInt("lastSceneIndex", currentSceneIndex);
            PlayerPrefs.Save();
        }
    }

    private void OnApplicationQuit()
    {
        SaveSceneIndexOnQuit();
    }
}