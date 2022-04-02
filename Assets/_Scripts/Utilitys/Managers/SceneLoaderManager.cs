using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoaderManager : MonoBehaviour
{
    private static bool gameEnded { get; set; } = false;
    bool gameWon = false;
    public static int frameRate { get; set; } = 144;
    private static SceneLoaderManager _instance;


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
