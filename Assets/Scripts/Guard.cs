using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    //1: x, 2: z
    [SerializeField]float moveSpeed;

    [SerializeField] Transform leftMostPosition;
    [SerializeField] Transform rightMostPosition;

    [SerializeField] bool moveOnXAxis = true;

    Animator animator;

    bool moveRight = false;
    public bool shouldNotWalk = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        shouldNotWalk = isPlaying(animator, "Look Around");

        if(!shouldNotWalk)
        {
            if(moveOnXAxis)
            {
                if(moveRight)
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }
            }
            else
            {
                if(moveRight)
                {
                    MoveForward();
                }
                else
                {
                    MoveBackward();
                }
            }
        }
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    public void LookForPlayer()
    {
        // animator.SetBool("isLookingAround", true);
        animator.SetTrigger("lookAroundTrigger");
    }

    private void MoveBackward()
    {
        transform.forward = -Vector3.forward;
        transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        if (transform.position.z < leftMostPosition.position.z)
        {
            moveRight = true;
        }
    }

    private void MoveForward()
    {
        transform.forward = Vector3.forward;
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        if (transform.position.z > rightMostPosition.position.z)
        {
            moveRight = false;
        }
    }

    private void MoveLeft()
    {
        transform.forward = Vector3.left;
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        if (transform.position.x < leftMostPosition.position.x)
        {
            moveRight = true;
        }
    }

    private void MoveRight()
    {
        transform.forward = Vector3.right;
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        if (transform.position.x > rightMostPosition.position.x)
        {
            moveRight = false;
        }
    }
}
