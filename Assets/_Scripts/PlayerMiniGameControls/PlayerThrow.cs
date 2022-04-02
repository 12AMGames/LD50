using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : PlayerControls
{
    public override void MouseUp()
    {
        base.MouseUp();
        line.enabled = false;
        if (realCoolDownTime >= 0)
            return;
        realCoolDownTime = coolDownTime;
        mouseLaunchDir = mouseStartPos - mouseEndPos;
        instantiatedObject = Instantiate(throwObject, transform.position, Quaternion.Euler(mouseLaunchDir));
        //instantiatedObject.GetComponent<Rigidbody2D>().AddForce(throwPower * mouseLaunchDir, ForceMode2D.Impulse);
        instantiatedObject.GetComponent<Rigidbody2D>().velocity = (mouseLaunchDir);
    }
}
