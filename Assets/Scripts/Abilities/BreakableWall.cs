using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(playerMovement.isStrong)
            {
                BreakWall();
            }
        }
    }

    void BreakWall()
    {
        this.gameObject.SetActive(false);
    }
}
