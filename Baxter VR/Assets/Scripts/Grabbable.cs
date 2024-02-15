using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// *************************************************************************
// Purpose: Grabbable is a subclass of AbstractGrabbable encompassing any 
//          object that can be picked up, held, dropped, and thrown, but 
//          lacks any special characteristics or operations. Examples of 
//          objects that would be given a Grabbable script include pieces 
//          of paper, inanimate spider toys, and other trinkets in the rooms' 
//          desks or dresser drawers.
// *************************************************************************
public class Grabbable : AbstractGrabbable
{   
    // ******************************************************************
    // Functionality: Start is called before the first frame update.
    //                Sets the isGrabbed variable to false by default and
    //                instantiates the rigidibody component.
    //                                                              
    // Parameters: none                                             
    // Return: none                                                 
    // ******************************************************************
    public override void Start()
    {
        isGrabbed = false;
        canBeGrabbed = true;
        initialParent = transform.parent;
        rigidbody = GetComponent<Rigidbody>();

        if (GetComponent<Renderer>() != null)
            initialMaterial = GetComponent<Renderer>().material;
    }


    // ********************************************
    // Functionality: Object is picked up by user, 
    //                and object is rotated to fit
    //                user hand.
    //
    // Parameters: none
    // Return: none
    // ********************************************
    public override void WhileGrabbed()
    {
        updateIterations++;
        this.RemoveHighlight();
        rigidbody.isKinematic = true;

        if (updateIterations == 10)
        {
            updateIterations = 0;
            
            previousPosition = transform.position;
            previousRotation = transform.rotation;
        }
    }


    // ******************************************************************************
    // Functionality: Calculates the angular velocity and velocity of the rigid body,
    //                and sets the rigid body velocity, angular velocity to those values upon release. 
    //                Also sets drag to 0 for indoor purposes. 
    // 
    // Parameters: none
    // Return: none
    // ******************************************************************************
    public override void WhenReleased()
    {
        this.SetGrabStatus(false, null);

        if (canBeGrabbed)
        {
            transform.parent = initialParent;
            rigidbody.isKinematic = false;

            foreach (Collider col in GetComponents<Collider>())
                col.isTrigger = false;

            Vector3 velocity = (transform.position - previousPosition) / (10 * Time.deltaTime);
            Vector3 angularVelocity = (transform.rotation.eulerAngles - previousRotation.eulerAngles) / (5 * Time.deltaTime);

            rigidbody.drag = 0;
            rigidbody.velocity = velocity;
            rigidbody.angularVelocity = angularVelocity;

            previousPosition = transform.position;
            previousRotation = transform.rotation;
        }
    }
}