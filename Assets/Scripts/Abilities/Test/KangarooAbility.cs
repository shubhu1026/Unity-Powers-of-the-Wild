using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KangarooAbility : AbilityAbstract
{
    public override void Activate()
    {
        base.Activate();//do everything in abilityabstract
        Debug.Log("start kangaroo");
    }
    public override void Deactivate()
    {
        base.Deactivate();
        
    }
}
