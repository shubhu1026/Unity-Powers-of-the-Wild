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
        Debug.Log("Burrow");
        playerMovement = parent.GetComponent<PlayerMovement>();
        collider = parent.GetComponentInChildren<CapsuleCollider>();
        originalCenter = collider.center;
        originalHeight = collider.height;

        // playerMovement.isBurrowing = true;

        collider.height = 0.3f;
        collider.center = new Vector3(collider.center.x, 1.5f, collider.center.z);
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        collider.height = originalHeight;
        collider.center = originalCenter;

        parent.GetComponent<PlayerMovement>().Jump();
    }
}
