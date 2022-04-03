using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class PlayerControls : MonoBehaviour
{
    protected Controls controls;

    public Sprite[] playerSprites;
    protected float lookAngle = 0f;
    protected bool mouseDown = false, overUI;
    protected Vector2 mousePos, mouseStartPos, mouseEndPos, ogPos;
    protected SpriteRenderer sr;

    private void Awake()
    {
        controls = new Controls();
        controls.normal.MousePos.performed += ctx => mousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
        controls.normal.Click.performed += _ => MouseDown();
        controls.normal.Click.canceled += _ => MouseUp();        
    
        sr = GetComponent<SpriteRenderer>();
    
        ogPos = transform.position;
    }

 
    private void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Intro:
                break;
            case GameState.Playing:
                controls.Enable();
                break;
            case GameState.LevelWin:
                controls.Disable();
                break;
            case GameState.LevelLose:
                sr.sprite = playerSprites[0];
                controls.Disable();
                break;
            default:
                Debug.Log("ruh roh");
                break;
        }
    }

    public IEnumerator ShakePlayer()
    {
        float _timer = 0.1f;
        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            transform.position += (Vector3)randDir();
            yield return null;
        }
        transform.position = ogPos;
        yield break;
    }

    Vector2 randDir()
    {
        Vector2 dir = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        return dir * 0.1f;
    }

    public virtual void MouseDown()
    {
        mouseDown = true;
        mouseStartPos = mousePos;
    }

    public virtual void MouseUp()
    {
        mouseEndPos = mousePos;
        mouseDown = false;
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChange += OnGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnGameStateChange;
        controls.Disable();
    }
}
