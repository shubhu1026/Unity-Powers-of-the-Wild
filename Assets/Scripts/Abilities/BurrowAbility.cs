using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BurrowAbility : Ability
{
    PlayerMovement playerMovement;
    CapsuleCollider collider;

    Vector3 originalCenter;
    float originalHeight;

    public override void Activate(GameObject parent)
    {
        Debug.Log("Burrow activated");
        playerMovement = parent.GetComponent<PlayerMovement>();
        collider = parent.GetComponentInChildren<CapsuleCollider>();
        originalCenter = collider.center;
        originalHeight = collider.height;

        playerMovement.isBurrowing = true;

        collider.height = 0.3f;
        collider.center = new Vector3(collider.center.x, 1.7f, collider.center.z);
        SFX.instance.PlaySFX(sound, playerMovement.transform.position);
        parent.GetComponent<ParticlePlsyer>().PlaySkillParticle();
    }

    public override void Active(GameObject parent)
    {
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        Debug.Log("Burrow deactivated");
        collider.height = originalHeight;
        collider.center = originalCenter;
        playerMovement.isBurrowing = false;
        playerMovement.Jump();
    }
}
