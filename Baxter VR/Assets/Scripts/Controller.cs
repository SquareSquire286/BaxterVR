using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hand
{
    Right, // right beans
    Left // left beans
}

public class Controller : MonoBehaviour // Controller beans
{
    [SerializeField] Hand hand;

    public Hand GetHand()
    {
        return hand;
    }
}
