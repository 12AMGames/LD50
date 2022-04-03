using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCpsBase : PlayerControls
{
    int cps = 0;
    int playerStrength = 3;
    float tick = 0f;

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameState != GameState.Playing)
            return;

        if (tick < 1)
        {
            tick += Time.fixedDeltaTime;
        }
        else
        {
            if(cps < 4)
            {
                playerStrength--;
            }
            if(playerStrength <= 0)
            {
                GameManager.Instance.UpdateGameState(GameState.LevelLose);
            }
            cps = 0;
            tick = 0;
        }
    }

    public override void MouseUp()
    {
        cps++;
        AudioManager.instance.Play("PlayerReady");
        StartCoroutine("ShakePlayer");
        base.MouseUp();
    }
}
