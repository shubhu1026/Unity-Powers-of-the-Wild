using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GrowAbility : Ability
{
    [Range(0,1)]public float increaseInScale;

    Vector3 originalScale;
    Vector3 targetScale;
    Transform playerTransform;
    Vector3 scale;

    PlayerMovement playerMovement;

    public override void Activate(GameObject parent)
    {
        Debug.Log("GROW AbILITY");
        playerTransform = parent.GetComponentInParent<Transform>();
        playerMovement = parent.GetComponent<PlayerMovement>();
        originalScale = parent.transform.localScale;
        targetScale = originalScale * increaseInScale;

        playerTransform.localScale = targetScale;
        playerMovement.playerHeight *= 0.5f;

        playerMovement.isStrong = true;
        SFX.instance.PlaySFX(sound, playerMovement.transform.position);
        parent.GetComponent<ParticlePlsyer>().PlaySkillParticle();
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        playerTransform.localScale = originalScale;
        playerMovement.playerHeight *= playerMovement.OriginalPlayerHeight;
        playerMovement.isStrong = false;
    }
}
