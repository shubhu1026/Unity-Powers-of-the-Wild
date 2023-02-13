using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CheetahAbility : Ability
{
    [Range(0,100)]public float speedIncreasePercentage;
    
    PlayerMovement playerMovement;
    float originalMovementSpeed;

    public override void Activate(GameObject parent)
    {
        playerMovement = parent.GetComponent<PlayerMovement>();
        originalMovementSpeed = playerMovement.baseMovementSpeed;
        playerMovement.baseMovementSpeed += (speedIncreasePercentage * originalMovementSpeed)/100;
        Debug.Log("move speed: " + playerMovement.baseMovementSpeed);
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        playerMovement.baseMovementSpeed = originalMovementSpeed;
        Debug.Log("move speed: " + playerMovement.baseMovementSpeed);
    }
}
