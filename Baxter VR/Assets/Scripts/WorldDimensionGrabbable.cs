using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class WorldDimensionGrabbable : AbstractGrabbable
{
    public bool useLocalXAxis; // if false, use local Z axis (i.e., back wall)
    public float minLocalAxisValue, maxLocalAxisValue;
    public WallMovementModule wallMovementModule;
    public GameObject wallRepresented;

    public override void Start()
    {
        isGrabbed = false;
        canBeGrabbed = true;

        if (GetComponent<Renderer>() != null)
            initialMaterial = GetComponent<Renderer>().material;
    }

    public override void SetGrabStatus(bool newStatus, GameObject hand)
    {
        handGrabbingMe = hand;
        isGrabbed = newStatus;

        if (GetGrabStatus())
        {
            this.RemoveHighlight();

            previousPosition = transform.position;
            previousRotation = transform.rotation;
        }

        else
        {
            isGrabbed = false;
            handGrabbingMe = null;
        }
    }

    public override void WhileGrabbed()
    {
        this.RemoveHighlight();

        if (useLocalXAxis)
        {
            transform.localPosition = new Vector3(Mathf.Clamp(transform.parent.InverseTransformPoint(handGrabbingMe.transform.position).x, minLocalAxisValue, maxLocalAxisValue), transform.localPosition.y, transform.localPosition.z);
            wallMovementModule.UpdateWall(wallRepresented, transform.localPosition.x, minLocalAxisValue, maxLocalAxisValue);
        }

        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Clamp(transform.parent.InverseTransformPoint(handGrabbingMe.transform.position).z, minLocalAxisValue, maxLocalAxisValue));
            wallMovementModule.UpdateWall(wallRepresented, transform.localPosition.z, minLocalAxisValue, maxLocalAxisValue);
        }
    }

    public override void WhenReleased()
    {
        this.SetGrabStatus(false, null);
    }
}
