using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LowSpeedAbility : Ability
{
    [Range(0,100)]public float speedDecreasePercentage;
    
    PlayerMovement playerMovement;
    float originalMovementSpeed;

    public override void Activate(GameObject parent)
    {
        playerMovement = parent.GetComponent<PlayerMovement>();
        playerMovement.MovementSpeed -= (speedDecreasePercentage * playerMovement.OriginalMovementSpeed)/100;
        Debug.Log("move speed: " + playerMovement.MovementSpeed);
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        playerMovement.MovementSpeed = playerMovement.OriginalMovementSpeed;
        Debug.Log("move speed: " + playerMovement.MovementSpeed);
    }
}
