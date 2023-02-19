using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StickyAbility : Ability
{
    GlueTest glueObject;
    [SerializeField] LayerMask layer_mask;

    public override void Activate(GameObject parent)
    {
        Debug.Log("spider web activated");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        parent.GetComponent<ParticlePlsyer>().PlaySkillParticle();
    }

    public override void Active(GameObject parent)
    {
        if(Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity , layer_mask))
            {
                var selection = hit.transform;
                if(selection.TryGetComponent<GlueTest>(out glueObject))
                {
                    SFX.instance.PlaySFX(sound, glueObject.transform.position);
                    glueObject.isSticky = true;
                }
            }
        }
    }

    public override void ResetAbilityChanges(GameObject parent)
    {
        Debug.Log("spider web deactivated");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
