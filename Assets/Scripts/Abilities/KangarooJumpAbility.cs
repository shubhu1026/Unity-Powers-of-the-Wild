using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class KangarooJumpAbility : Ability
{
    PlayerMovement playerMovement;

    public override void Activate(GameObject parent)
    {
        Debug.Log("K jump activated");
        playerMovement = parent.GetComponent<PlayerMovement>();
        playerMovement.highJump = true;
        SFX.instance.PlaySFX(sound, playerMovement.transform.position);
        parent.GetComponent<ParticlePlsyer>().PlaySkillParticle();
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        Debug.Log("K jump deactivated");
        playerMovement.highJump = false;
    }
}
