using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RatAbility : Ability
{
    [Range(0,1)]public float decreaseInScale;

    Vector3 originalScale;
    Vector3 targetScale;
    Transform playerTransform;
    Vector3 scale;

    PlayerMovement playerMovement;

    public override void Activate(GameObject parent)
    {
        Debug.Log("rat activated");
        playerTransform = parent.GetComponentInParent<Transform>();
        playerMovement = parent.GetComponent<PlayerMovement>();
        originalScale = parent.transform.localScale;
        targetScale = originalScale * decreaseInScale;

        playerTransform.localScale = targetScale;
        playerMovement.playerHeight *= 0.5f;
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        Debug.Log("rat deactivated");
        playerTransform.localScale = originalScale;
        playerMovement.playerHeight *= playerMovement.OriginalPlayerHeight;
    }
}
