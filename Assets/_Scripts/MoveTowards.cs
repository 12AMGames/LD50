using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float speed = 2f;
    float realSpeed = 2f;

    private void Awake()
    {
        
    }

    private void Start()
    {
        realSpeed = speed;
    }

    private void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Intro:
                break;
            case GameState.Playing:
                StartCoroutine("moveTowards");
                break;
            case GameState.LevelEnd:
                break;
        }
    }

    IEnumerator moveTowards()
    {
        while(Vector3.Distance(transform.position, target.position) > 0.1)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, realSpeed * Time.deltaTime);
            yield return null;
        }
        yield break;
    }

    IEnumerator Stun()
    {
        realSpeed = 0;
        float time = 1;
        while(time > 0)
        {
            time -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (Vector3.up + Vector3.right), speed * Time.deltaTime);
            yield return null;
        }
        realSpeed = speed;
        yield break;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            StartCoroutine("Stun");
            Destroy(collision.gameObject);
        }
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
