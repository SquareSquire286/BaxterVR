using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// ***********************************************************************
// Purpose: The interface for all objects 
//          that can be grabbed and held by the user.
// 
// Class Variables: 
//                   isGrabbed -> dictates the objects per
//                                frame behaviour
//
//                   handGrabbingMe -> the HandAnchor game object that is 
//                                     currently holding the object
// ***********************************************************************
public abstract class AbstractGrabbable : MonoBehaviour
{
    public bool isGrabbed; 
    public GameObject handGrabbingMe;
    public Material initialMaterial;
    public Rigidbody rigidbody;
    public Vector3 previousPosition;
    public Quaternion previousRotation;
    public int updateIterations;
    protected bool canBeGrabbed;

    public List<PlayerScale> validScalesForGrabbing;

    protected Transform initialParent;

    // ************************************************************
    // Functionality: Start is called before the first frame update
    // 
    // Parameters: none
    // return: none
    // ************************************************************
    public virtual void Start()
    {
        initialParent = transform.parent;
    }


    // ****************************************************************************
    // Functionality: Update is called once per frame. If the object's
    //                grab status is true, then execute its while-grabbed behaviour
    //                on a per frame basis
    // 
    // Parameters: none
    // return: none
    // ****************************************************************************
    public virtual void Update()
    {
        if (isGrabbed)
            WhileGrabbed();
    }


    // ****************************************************************************
    // Functionality: If the object's grab status chnages from True to False, 
    //                execute the object's while-not-grabbed behaviour
    // 
    // Parameters: none
    // return: none
    // ****************************************************************************
    public virtual void SetGrabStatus(bool newStatus, GameObject hand)
    {
        handGrabbingMe = hand;
        isGrabbed = newStatus;

        if (GetGrabStatus())
        {
            transform.parent = handGrabbingMe.transform;
            rigidbody.isKinematic = true;

            foreach (Collider col in GetComponents<Collider>())
                col.isTrigger = true;

            this.RemoveHighlight();

            previousPosition = transform.position;
            previousRotation = transform.rotation;
        }

        else
        {
            isGrabbed = false;
            transform.parent = initialParent;
            handGrabbingMe = null;
        }
    }


    // ****************************************************************************
    // Functionality: Called by Grabber whenever a candidate object is 
    //                found for a HandAnchor to hold - the last condition is that
    //                the objectmust not alreadybe held by the other HandAnchor
    // 
    // Parameters: none
    // return: isGrabbed - boolean
    // ****************************************************************************
    public virtual bool GetGrabStatus() 
    {
        if (canBeGrabbed)
            return isGrabbed;

        else return canBeGrabbed;
    }


    // ****************************************************************************
    // Functionality: Defined in concrete subclasses - called every 
    //                frame that isGrabbed is true
    // 
    // Parameters: none
    // return: none
    // ****************************************************************************
    public virtual void WhileGrabbed()
    {

    }


    // ****************************************************************************
    // Functionality: Defined in concrete subclasses - called on the 
    //                exact frame that isGrabbed becomes false
    // 
    // Parameters: none
    // return: none
    // ****************************************************************************
    public virtual void WhenReleased()
    {
        
    }

    public virtual void ApplyHighlight(Material highlightMaterial)
    {
        bool scaleIsValid = false;

        foreach (PlayerScale scale in validScalesForGrabbing)
        {
            if (scale == PlayerScaleSingleton.GetPlayerScale())
                scaleIsValid = true;
        }

        if (!scaleIsValid)
            canBeGrabbed = false;

        else
        {
            canBeGrabbed = true;

            if (GetComponent<Renderer>() == null)
            {
                if (transform.childCount > 0)
                    transform.GetChild(0).gameObject.GetComponent<Renderer>().material = highlightMaterial;

                else transform.parent.gameObject.GetComponent<Renderer>().material = highlightMaterial;
            }

            else GetComponent<Renderer>().material = highlightMaterial;
        }
        
    }

    public virtual void RemoveHighlight()
    {
        if (GetComponent<Renderer>() == null)
        {
            if (transform.childCount > 0)
                transform.GetChild(0).gameObject.GetComponent<Renderer>().material = initialMaterial;

            else transform.parent.gameObject.GetComponent<Renderer>().material = initialMaterial;
        } 

        else GetComponent<Renderer>().material = initialMaterial;
    }
}
