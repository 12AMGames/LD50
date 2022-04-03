using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float rate = 2f;

    private void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Intro:
                break;
            case GameState.Playing:
                StartCoroutine("SpawnBullets");
                break;
            case GameState.LevelWin:
                StopAllCoroutines();
                break;
            case GameState.LevelLose:
                break;
            default:
                Debug.Log("ruh roh");
                break;
        }
    }

    IEnumerator SpawnBullets()
    {
        while(GameManager.Instance.gameState == GameState.Playing) 
        {
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            yield return new WaitForSeconds(rate);
            Instantiate(projectile, point.position, point.rotation);
            yield return null;
        }
        yield break;
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChange += OnGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnGameStateChange;
    }
}
