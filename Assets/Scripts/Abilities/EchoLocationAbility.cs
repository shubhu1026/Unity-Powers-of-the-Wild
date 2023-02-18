using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EchoLocationAbility : Ability
{
    PlayerMovement playerMovement;

    public override void Activate(GameObject parent)
    {
        playerMovement = parent.GetComponent<PlayerMovement>();
        playerMovement.echoLocation = true;
        SFX.instance.PlaySFX(sound, playerMovement.transform.position);
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        playerMovement.echoLocation = false;
    }
}
