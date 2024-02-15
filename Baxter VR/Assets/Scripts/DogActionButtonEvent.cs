using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogActionButtonEvent : AbstractButtonEvent
{
    public DogState stateToTrigger;
    public DogModelFSM dogModel;

    public override void ExecuteEvent()
    {
        switch (stateToTrigger)
        {
            case DogState.Jump: dogModel.Jump(); break;
            case DogState.Stand: dogModel.Stand(); break;
            case DogState.LieDown: dogModel.LieDown(); break;
            default: dogModel.Sit(); break;
        }
    }
}
