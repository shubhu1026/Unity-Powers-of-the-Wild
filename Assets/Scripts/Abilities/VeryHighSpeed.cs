using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VeryHighSpeed : Ability
{
    [Range(0,100)]public float speedIncreasePercentage;
    
    PlayerMovement playerMovement;
    float originalMovementSpeed;

    public override void Activate(GameObject parent)
    {
        playerMovement = parent.GetComponent<PlayerMovement>();
        playerMovement.MovementSpeed += ((speedIncreasePercentage * playerMovement.OriginalMovementSpeed)/100) * 2;
        Debug.Log("move speed: " + playerMovement.MovementSpeed);
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        playerMovement.MovementSpeed = playerMovement.OriginalMovementSpeed;
        Debug.Log("move speed: " + playerMovement.MovementSpeed);
    }
}
