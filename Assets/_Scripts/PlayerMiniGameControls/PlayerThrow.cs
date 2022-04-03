using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : PlayerControls
{
    [SerializeField] GameObject throwObject = null;
    [SerializeField] LineRenderer line;
    [SerializeField] float throwPower = 2f, coolDownTime;
    float realCoolDownTime;
    GameObject instantiatedObject = null;
    Vector2 mouseLaunchDir;

    private void Start()
    {
        realCoolDownTime = coolDownTime;
        line.enabled = false;
    }

    private void Update()
    {
        if (realCoolDownTime >= 0)
        {
            realCoolDownTime -= Time.deltaTime;
            sr.sprite = playerSprites[1];
            return;
        }

        if (mouseDown)
        {
            line.enabled = true;
            sr.sprite = playerSprites[2];
            Vector3[] trajectory = plotArc(throwObject.GetComponent<Rigidbody2D>(), (Vector2)transform.position, mouseStartPos - mousePos, 250);
            line.positionCount = trajectory.Length;
            line.SetPositions(trajectory);


            Vector2 mouseScreenPos = mousePos - (Vector2)transform.position;
            lookAngle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        }
        mouseLaunchDir = mouseStartPos - mouseEndPos;
    }

    public override void MouseDown()
    {
        base.MouseDown();
        if (realCoolDownTime >= 0)
            return;
        AudioManager.instance.Play("PlayerReady");
    }

    public override void MouseUp()
    {
        base.MouseUp();
        line.enabled = false;
        if (realCoolDownTime >= 0)
            return;
        sr.sprite = playerSprites[3];
        AudioManager.instance.Play("PlayerThrow");
        realCoolDownTime = coolDownTime;
        mouseLaunchDir = mouseStartPos - mouseEndPos;
        instantiatedObject = Instantiate(throwObject, transform.position, Quaternion.Euler(mouseLaunchDir));
        instantiatedObject.GetComponent<Rigidbody2D>().velocity = (mouseLaunchDir);
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
}
