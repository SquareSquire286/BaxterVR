using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerScaleButton : AbstractButton
{
    public bool startPressed;

    public override void Start()
    {
        initialMaterial = GetComponent<Renderer>().material;
        isPressed = startPressed;

        if (isPressed)
            transform.localPosition = pressedPosition;

        else transform.localPosition = releasedPosition;
    }
}
