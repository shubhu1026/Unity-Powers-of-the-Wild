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
        playerMovement = parent.GetComponent<PlayerMovement>();
        playerMovement.JumpForce = playerMovement.BaseJumpForce * jumpMultiplier;
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        playerMovement.JumpForce = playerMovement.BaseJumpForce;
    }
}
