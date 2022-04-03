using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject bulletBreakEffect;
    [SerializeField] float bulletSpeed = 1;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = bulletSpeed * -transform.right;
    }

    private void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Intro:
                break;
            case GameState.Playing:
                break;
            case GameState.LevelWin:
                BreakBullet();
                break;
            case GameState.LevelLose:
                BreakBullet();
                break;
            default:
                Debug.Log("ruh roh");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BreakBullet();
    }

    void BreakBullet()
    {
        Instantiate(bulletBreakEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
