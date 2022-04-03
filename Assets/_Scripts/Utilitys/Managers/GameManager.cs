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
            case GameState.LevelWin:
                SceneLoaderManager.Instance.gamesComplete++;
                levelCatchPhraseText.enabled = true;
                levelCatchPhraseText.text = Phrases.getRandomWinPhrase();
                break;
            case GameState.LevelLose:
                SceneLoaderManager.Instance.gamesComplete = 0;
                levelCatchPhraseText.enabled = true;
                levelCatchPhraseText.text = Phrases.getRandomLosePhrase();
                break;
            default:
                Debug.LogError("Somethings wrong I can feel it");
                break;
        }

        OnGameStateChange?.Invoke(newState);
    }

    IEnumerator countDown()
    {
        float timer = 5f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            levelCountdownText.text = ((int)timer).ToString();
            yield return null;
        }
        UpdateGameState(GameState.Playing);
        float timer2 = 20f;
        while (timer2 > 0)
        {
            timer2 -= Time.deltaTime;
            levelCountdownText.text = ((int)timer2).ToString();
            if(gameState == GameState.LevelLose)
            {
                AudioManager.instance.Play("PlayerDie");
                yield return new WaitForSeconds(3);
                SceneLoaderManager.Instance.ChooseRandMiniGame();
            }
            yield return null;
        }
        UpdateGameState(GameState.LevelWin);
        yield return new WaitForSeconds(3);
        SceneLoaderManager.Instance.ChooseRandMiniGame();
    }
}

public enum GameState 
{
    Intro,
    Playing,
    LevelWin,
    LevelLose
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

