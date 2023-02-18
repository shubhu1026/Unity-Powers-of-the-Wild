using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StickyAbility : Ability
{
    GlueTest glueObject;

    public override void Activate(GameObject parent)
    {
        Debug.Log("spider web activated");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void Active(GameObject parent)
    {
        if(Input.GetMouseButtonDown(0))
        {
            SFX.instance.PlaySFX(sound, glueObject.transform.position);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if(selection.CompareTag("Stickable"))
                {
                    glueObject = selection.GetComponent<GlueTest>();
                    // glueObject.isSticky = !glueObject.isSticky;
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
