using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    //1: x, 2: z
    [SerializeField]float moveSpeed;

    [SerializeField] Transform leftMostPosition;
    [SerializeField] Transform rightMostPosition;

    bool moveRight = false;

    Vector3 playerPosition;
    Vector3 leftPosition;
    Vector3 rightPosition;

    void Awake()
    {
            if((transform.position - leftMostPosition.position).magnitude > (transform.position - rightMostPosition.position).magnitude)
            {
                moveRight = true;
            }

        leftPosition = new Vector3(leftMostPosition.position.x, 0, leftMostPosition.position.z);
        rightPosition = new Vector3(rightMostPosition.position.x, 0, rightMostPosition.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(transform.position.x, 0, transform.position.z);

        if(moveRight)
        {
            transform.Translate((rightPosition - playerPosition).normalized * moveSpeed * Time.deltaTime);
            if((rightPosition - playerPosition).magnitude < 2)
            {
                moveRight = false;
            }
        }
        else
        {
            transform.Translate((leftPosition - playerPosition).normalized * moveSpeed * Time.deltaTime);
            if((leftPosition - playerPosition).magnitude < 2)
            {
                moveRight = false;
            }
        }
    }
}
