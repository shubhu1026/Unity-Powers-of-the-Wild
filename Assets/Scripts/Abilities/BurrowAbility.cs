using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BurrowAbility : Ability
{
    PlayerMovement playerMovement;
    CharacterController characterController;

    Vector3 originalCenter;
    float originalHeight;

    public override void Activate(GameObject parent)
    {
        Debug.Log("Burrow");
        playerMovement = parent.GetComponent<PlayerMovement>();
        characterController = parent.GetComponent<CharacterController>();
        originalCenter = characterController.center;
        originalHeight = characterController.height;

        playerMovement.isBurrowing = true;

        characterController.height = 0.3f;
        characterController.center = new Vector3(characterController.center.x, 1.5f, characterController.center.z);
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        characterController.height = originalHeight;
        characterController.center = originalCenter; 
        // playerMovement.isJumpPressed = true;
        playerMovement.isBurrowing = false;
        
        characterController.Move(new Vector3(0, 2, 0));
    }
}
