using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RatAbility : Ability
{
    [Range(0,1)]public float decreaseInScale;

    Vector3 originalScale;
    Vector3 targetScale;
    Transform parentTransform;
    Vector3 scale;

    float shrinkTime = 2f;
    float shrinkRate = 7f;

    public override void Activate(GameObject parent)
    {
        Debug.Log("RAT AbILITY");
        originalScale = parent.transform.localScale;
        targetScale = originalScale * decreaseInScale;

        // parent.transform.localScale = targetScale;
        parentTransform = parent.transform;

        if(shrinkTime > 0)
        {
            shrinkTime -= Time.deltaTime;
            parentTransform.localScale -= new Vector3(0.2F, .2f, .2f) * shrinkRate;
        }
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        if(shrinkTime > 0)
        {
            shrinkTime -= Time.deltaTime;
            parentTransform.localScale += new Vector3(0.2F, .2f, .2f) * shrinkRate;
        }
        // parent.transform.localScale = originalScale;
    }
}
