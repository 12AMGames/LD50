using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("ManagerNull");
            }
            return _instance;
        }
    }

    public GameState gameState;

    //UI
    [SerializeField] string levelCatchphrase;
    GameObject uI;
    TextMeshProUGUI levelCatchPhraseText;
    TextMeshProUGUI levelCountdownText;

    //Events
    public static event Action<GameState> OnGameStateChange;

    GameObject levelWinUI;
    //public Transform playerTransform;

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
    }

    private void Start()
    {
        uI = GameObject.FindGameObjectWithTag("UI");
        levelCatchPhraseText = uI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        levelCountdownText = uI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        UpdateGameState(GameState.Intro);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.Intro:
                StartCoroutine("countDown");
                levelCatchPhraseText.text = levelCatchphrase;
                break;
;            case GameState.Playing:
                levelCountdownText.color = Color.grey;
                levelCatchPhraseText.enabled = false;
                break;
            case GameState.LevelEnd:
                break;
            default:
                Debug.LogError("Somethings wrong I can feel it");
                break;
        }

        OnGameStateChange?.Invoke(newState);
    }

    IEnumerator countDown()
    {
        float timer = 3f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            levelCountdownText.text = ((int)timer).ToString();
            yield return null;
        }
        UpdateGameState(GameState.Playing);
        float timer2 = 30f;
        while (timer2 > 0)
        {
            timer2 -= Time.deltaTime;
            levelCountdownText.text = ((int)timer2).ToString();
            yield return null;
        }
        UpdateGameState(GameState.LevelEnd);
        yield return new WaitForSeconds(3);
        SceneLoaderManager.Instance.NextScene(2);
    }
}

public enum GameState 
{
    Intro,
    Playing,
    LevelEnd
}

public enum MiniGame
{
    CatThrower,
    DeathDodge,
    Invasion,
    Bailer,
    SkullSmasher,
    ArmWrestling,
    tasteTester
}

