using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogModelFSM : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sit()
    {
        animator.SetBool("Sit", true);
        animator.SetBool("Stand", false);
        animator.SetBool("LieDown", false);
        animator.SetBool("Jump", false);
    }

    public void Stand()
    {
        animator.SetBool("Stand", true);
        animator.SetBool("Sit", false);
        animator.SetBool("LieDown", false);
        animator.SetBool("Jump", false);
    }

    public void LieDown()
    {
        animator.SetBool("LieDown", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Jump", false);
    }

    public void Jump()
    {
        animator.SetBool("Jump", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Sit", false);
        animator.SetBool("LieDown", false);
    }
}
