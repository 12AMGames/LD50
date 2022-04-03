using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ClickToSkip : PlayerControls
{
    [SerializeField] UnityEvent onClick;

    private void Start()
    {
        controls.Enable();
    }

    public override void MouseDown()
    {
        base.MouseDown();
        onClick?.Invoke();
    }
}

