using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GrowAbility : Ability
{
    [Range(1,5)]public float increaseInScale;

    Vector3 originalScale;
    Vector3 targetScale;
    Transform parentTransform;
    Vector3 scale;

    float shrinkTime = 1f;
    float shrinkRate = 3f;

    public override void Activate(GameObject parent)
    {
        Debug.Log("RAT AbILITY");
        originalScale = parent.transform.localScale;
        targetScale = originalScale * increaseInScale;

        parent.transform.localScale = targetScale;
        // parentTransform = parent.transform;

        // if(shrinkTime > 0)
        // {
        //     shrinkTime -= Time.deltaTime;
        //     parentTransform.localScale -= new Vector3(0.1F, .1f, .1f) * shrinkRate;
        // }
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        // if(shrinkTime > 0)
        // {
        //     shrinkTime -= Time.deltaTime;
        //     parentTransform.localScale += new Vector3(0.1F, .1f, .1f) * shrinkRate;
        // }
        parent.transform.localScale = originalScale;
    }
}
