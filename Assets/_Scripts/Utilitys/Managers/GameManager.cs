using UnityEngine;
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
    GameObject uI;
    TextMeshProUGUI levelCatchPhrase;
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
        levelCatchPhrase = uI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        levelCountdownText = uI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        UpdateGameState(GameState.Intro);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.Intro:
                levelCatchPhrase.text = "balls lol";
                break;
;            case GameState.Playing:
                break;
            case GameState.LevelEnd:
                break;
            default:
                Debug.LogError("Somethings wrong I can feel it");
                break;
        }

        OnGameStateChange?.Invoke(newState);
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

