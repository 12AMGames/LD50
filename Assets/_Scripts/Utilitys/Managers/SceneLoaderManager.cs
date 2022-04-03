using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoaderManager : MonoBehaviour
{
    private static bool gameEnded { get; set; } = false;
    public static int frameRate { get; set; } = 144;
    private static SceneLoaderManager _instance;
    public int gamesComplete; 


    public static SceneLoaderManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("ManagerNull");
            }
            return _instance;
        }
    }

    //SceneManagement
    [SerializeField] string nextScene;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (this != _instance)
        {
            Destroy(this.gameObject);
        }

        gamesComplete = PlayerPrefs.GetInt("CompleteCount", 0);

        Application.targetFrameRate = frameRate;
    }

    public void GameEnd()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            Restart();
        }
    }

    public void NextScene(int _nextScene)
    {
        if (nextScene.Length == 0)
        {
            SceneManager.LoadScene(_nextScene);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    public void ChooseRandMiniGame()
    {
        PlayerPrefs.SetInt("CompleteCount", gamesComplete);
        if(PlayerPrefs.GetInt("CompleteCount", 0) >= 3)
        {
            PlayerPrefs.DeleteAll();
            NextScene(5);
            return;
        }
        int randScene = Random.Range(1, 5);
        while(randScene == PlayerPrefs.GetInt("lastScene", 10))
        {
            randScene = Random.Range(1, 5);
        }
        PlayerPrefs.SetInt("lastScene", randScene);
        NextScene(randScene);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void QuitGaem()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
