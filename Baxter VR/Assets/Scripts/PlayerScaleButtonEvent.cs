using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScaleButtonEvent : AbstractButtonEvent
{
    public PlayerScale newPlayerScale;
    public WorldScaleModule worldScalingAnchor;

    public override void ExecuteEvent()
    {
        worldScalingAnchor.ChangeWorldScale(newPlayerScale);
    }
}
