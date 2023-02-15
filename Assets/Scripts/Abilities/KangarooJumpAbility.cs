using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class KangarooJumpAbility : Ability
{
    PlayerMovement playerMovement;
    [SerializeField] float jumpMultiplier = 2;

    public override void Activate(GameObject parent)
    {
        Debug.Log("K jump activated");
        playerMovement = parent.GetComponent<PlayerMovement>();
        playerMovement.JumpForce = playerMovement.BaseJumpForce * jumpMultiplier;
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        Debug.Log("K jump deactivated");
        playerMovement.JumpForce = playerMovement.BaseJumpForce;
    }
}
