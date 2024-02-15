using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class DogFSM : MonoBehaviour
{
    protected float movementSpeed;
    protected Animator animator;
    protected Rigidbody rigidbody;
    protected bool beginMovement, hitWall, leftWall, canReactToPlayer;
    protected Vector3 updatedOrientation;
    protected float rotationSpeedMagnifier;
    protected string playerTag = "Player", wallTag = "Wall";
    private DogState currentState;
    private AudioSource audioSource;
    public AudioClip standSound, barkSound, attackSound, sitSound, lieDownSound, runSound, sneakSound, trotSound, jumpSound, walkSound, sleepSound;

    // Start is called before the first frame update
    void Start()
    {
        canReactToPlayer = true;
        movementSpeed = 0.1f;
        rotationSpeedMagnifier = 0.75f;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        InvokeRepeating("ChooseAction", 3f, 6f);

        audioSource = GetComponent<AudioSource>();
        currentState = DogState.Stand;
        audioSource.clip = standSound;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beginMovement)
        {
            if (!hitWall && leftWall)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(updatedOrientation.x, updatedOrientation.y, updatedOrientation.z), movementSpeed * rotationSpeedMagnifier * Time.deltaTime);

            transform.localPosition += transform.forward * movementSpeed * Time.deltaTime;
        }
    }

    public virtual void ChooseAction()
    {
        int choice;

        switch (PlayerScaleSingleton.GetPlayerScale())
        {
            case PlayerScale.Giant:
                choice = UnityEngine.Random.Range(0, 6);
                switch (choice)
                {
                    case 0: Sneak(); break;
                    case 1: Bark(); break;
                    case 2: Trot(); break;
                    case 3: Run(); break;
                    case 4: Jump(); break;
                    default: Stand(); break;
                }
                break;
            case PlayerScale.Dog:
                choice = UnityEngine.Random.Range(0, 6);
                switch (choice)
                {
                    case 0: Trot(); break;
                    case 1: Run(); break;
                    case 2: Jump(); break;
                    case 3: Sneak(); break;
                    case 4: Bark(); break;
                    default: Stand(); break;
                }
                break;
            case PlayerScale.Flea:
                choice = UnityEngine.Random.Range(0, 5);
                switch (choice)
                {
                    case 0: Sit(); break;
                    case 1: Trot(); break;
                    case 2: Walk(); break;
                    case 3: Run(); break;
                    default: Stand(); break;
                }
                break;
            default:
                choice = UnityEngine.Random.Range(0, 5);
                switch (choice)
                {
                    case 0: Sit(); break;
                    case 1: LieDown(); break;
                    case 2: Walk(); break;
                    case 3: Trot(); break;
                    default: Stand(); break;
                }
                break;
        }
    }

    public void Walk()
    {
        currentState = DogState.Walk;

        audioSource.clip = walkSound;
        audioSource.Play();

        animator.SetBool("Walk", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Trot", false);
        animator.SetBool("Run", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Sneak", false);
        animator.SetBool("LieDown", false);
        animator.SetBool("Bark", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Sleep", false);

        int choice = UnityEngine.Random.Range(-180, 181);
        updatedOrientation = new Vector3(transform.rotation.x, transform.rotation.y + choice, transform.rotation.z);

        beginMovement = true;

        if (!hitWall)
        {
            leftWall = true;
        }
    }

    public void Sit()
    {
        currentState = DogState.Sit;

        audioSource.clip = sitSound;
        audioSource.Play();

        animator.SetBool("Sit", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Trot", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Sneak", false);
        animator.SetBool("LieDown", false);
        animator.SetBool("Bark", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Sleep", false);

        beginMovement = false;
    }

    public void Run()
    {
        currentState = DogState.Run;

        audioSource.clip = runSound;
        audioSource.Play();

        animator.SetBool("Run", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Trot", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Sneak", false);
        animator.SetBool("LieDown", false);
        animator.SetBool("Bark", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Sleep", false);

        int choice = UnityEngine.Random.Range(-180, 181);
        updatedOrientation = new Vector3(transform.rotation.x, transform.rotation.y + choice, transform.rotation.z);

        beginMovement = true;

        if (!hitWall)
        {
            leftWall = true;
        }
    }

    public void Stand()
    {
        currentState = DogState.Stand;

        audioSource.clip = standSound;
        audioSource.Play();

        animator.SetBool("Stand", true);
        animator.SetBool("Trot", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Sneak", false);
        animator.SetBool("LieDown", false);
        animator.SetBool("Bark", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Sleep", false);

        beginMovement = false;
    }

    public void LieDown()
    {
        currentState = DogState.LieDown;

        audioSource.clip = lieDownSound;
        audioSource.Play();

        animator.SetBool("LieDown", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Trot", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Sneak", false);
        animator.SetBool("Bark", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Sleep", false);

        beginMovement = false;
    }

    public void Bark()
    {
        currentState = DogState.Bark;

        audioSource.clip = barkSound;
        audioSource.Play();

        animator.SetBool("Bark", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Trot", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Sneak", false);
        animator.SetBool("LieDown", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Sleep", false);

        beginMovement = false;
    }

    public void Attack()
    {
        currentState = DogState.Attack;

        audioSource.clip = attackSound;
        audioSource.Play();

        animator.SetBool("Attack", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Trot", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Sneak", false);
        animator.SetBool("LieDown", false);
        animator.SetBool("Bark", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Sleep", false);

        beginMovement = false;

        Invoke("Bark", 1.0f);
    }

    public void Jump()
    {
        currentState = DogState.Jump;

        audioSource.clip = jumpSound;
        audioSource.Play();

        animator.SetBool("Jump", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Trot", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Sneak", false);
        animator.SetBool("LieDown", false);
        animator.SetBool("Bark", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Sleep", false);

        beginMovement = false;

        Invoke("Stand", 1.2f);
    }

    public void Trot()
    {
        currentState = DogState.Trot;

        audioSource.clip = trotSound;
        audioSource.Play();

        animator.SetBool("Trot", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Sneak", false);
        animator.SetBool("LieDown", false);
        animator.SetBool("Bark", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Sleep", false);

        int choice = UnityEngine.Random.Range(-180, 181);
        updatedOrientation = new Vector3(transform.rotation.x, transform.rotation.y + choice, transform.rotation.z);

        beginMovement = true;

        if (!hitWall)
        {
            leftWall = true;
        }
    }

    public void Sneak()
    {
        currentState = DogState.Sneak;

        audioSource.clip = sneakSound;
        audioSource.Play();

        animator.SetBool("Sneak", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Trot", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Sit", false);
        animator.SetBool("LieDown", false);
        animator.SetBool("Bark", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Sleep", false);

        int choice = UnityEngine.Random.Range(-180, 181);
        updatedOrientation = new Vector3(transform.rotation.x, transform.rotation.y + choice, transform.rotation.z);

        beginMovement = true;

        if (!hitWall)
        {
            leftWall = true;
        }
    }

    public void Sleep()
    {
        currentState = DogState.Sleep;

        audioSource.clip = sleepSound;
        audioSource.Play();

        animator.SetBool("Sleep", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Trot", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Sneak", false);
        animator.SetBool("LieDown", false);
        animator.SetBool("Bark", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Jump", false);

        beginMovement = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == wallTag)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + UnityEngine.Random.Range(90, 271), transform.rotation.eulerAngles.z);
            hitWall = true;
            leftWall = false;
        }

        else if (col.gameObject.tag == playerTag)
        {
            if (!canReactToPlayer)
                return;

            else
            {
                canReactToPlayer = false;

                if (PlayerScaleSingleton.GetPlayerScale() == PlayerScale.Human)
                {
                    int choice = UnityEngine.Random.Range(0, 3);
                    Invoke("ReenablePlayerReaction", 4f);

                    switch (choice)
                    {
                        case 0: Stand(); break;
                        case 1: Sit(); break;
                        default: LieDown(); break;
                    }
                }

                else if (PlayerScaleSingleton.GetPlayerScale() == PlayerScale.Dog)
                {
                    int choice = UnityEngine.Random.Range(0, 5);
                    Invoke("ReenablePlayerReaction", 3f);

                    switch (choice)
                    {
                        case 0: Stand(); break;
                        case 1: Sit(); break;
                        case 2: transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + UnityEngine.Random.Range(90, 271), transform.rotation.eulerAngles.z); hitWall = true; leftWall = false; Trot(); break;
                        case 3: transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + UnityEngine.Random.Range(90, 271), transform.rotation.eulerAngles.z); hitWall = true; leftWall = false; Run(); break;
                        default: Jump(); break;
                    }
                }

                else if (PlayerScaleSingleton.GetPlayerScale() == PlayerScale.Giant)
                {
                    int choice = UnityEngine.Random.Range(0, 4);
                    Invoke("ReenablePlayerReaction", 2f);

                    switch (choice)
                    {
                        case 0: Run(); break;
                        case 1: Jump(); break;
                        case 2: Bark(); break;
                        default: Attack(); break;
                    }
                }
            }
        }
    }

    public void ReenablePlayerReaction()
    {
        canReactToPlayer = true;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == wallTag)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + UnityEngine.Random.Range(90, 271), transform.rotation.eulerAngles.z);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == wallTag)
        {
            hitWall = false;
        }
    }
}