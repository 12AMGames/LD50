using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class PlayerControls : MonoBehaviour
{


    [SerializeField] GameObject throwObject = null;
    GameObject instantiatedObject = null;
    [SerializeField] LineRenderer line;
    [SerializeField] float throwPower = 2f, coolDownTime;
    float lookAngle = 0f, realCoolDownTime;
    Rigidbody2D rb;
    bool mouseDown = false, overUI;
    Vector2 mousePos, mouseStartPos, mouseEndPos, mouseLaunchDir;
    MiniGame currentMiniGame;

    

    private void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Intro:
                break;
            case GameState.Playing:
                controls.Enable();
                break;
            case GameState.LevelEnd:
                controls.Disable();
                break;
        }
    }

    private void Start()
    {
        realCoolDownTime = coolDownTime;
        line.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        //Cache if mouse is over UI, because who has time to write this more than once?
        //overUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    private void Update()
    {
        if(realCoolDownTime >= 0)
        {
            realCoolDownTime -= Time.deltaTime;
            return;
        }

        if (mouseDown)
        {
            line.enabled = true;

            Vector3[] trajectory = plotArc(throwObject.GetComponent<Rigidbody2D>(), (Vector2)transform.position, mouseStartPos - mousePos, 250);
            line.positionCount = trajectory.Length;
            line.SetPositions(trajectory);


            Vector2 mouseScreenPos = mousePos - (Vector2)transform.position;
            lookAngle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        }             
        mouseLaunchDir = mouseStartPos - mouseEndPos;
    }

    public void MouseDown()
    {
        mouseDown = true;
        mouseStartPos = mousePos;
        //switch (currentMiniGame)
        //{
        //    case MiniGame.CatThrower:
        //        break;
        //    case MiniGame.DeathDodge:
        //        break;
        //    case MiniGame.ArmWrestling:
        //        break;
        //    case MiniGame.tasteTester:
        //        break;
        //    case MiniGame.SkullSmasher:
        //        break;
        //    case MiniGame.Invasion:
        //        break;
        //    case MiniGame.Bailer:
        //        break;
        //    default:
        //        Debug.LogError("Ooopsie, somehow, you messed it up, like only you could");
        //        break;
        //}
    }

    public virtual void MouseUp()
    {
        mouseEndPos = mousePos;
        mouseDown = false;
    }

    //Math for calculating arc taken from "Trajectory prediction with Unity Physics" by space ape games
    Vector3[] plotArc(Rigidbody2D rb, Vector2 pos, Vector2 vel, int res)
    {
        Vector3[] results = new Vector3[res];

        float timeStep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rb.gravityScale * timeStep * timeStep;

        float drag = 1f - timeStep * rb.drag;
        Vector2 moveStep = vel * timeStep;

        for (int i = 0; i < res; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
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
