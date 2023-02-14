using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    private AbilityAbstract[] abilities;
    private AbilityAbstract currentAbility;
    public void SetAbility(AbilityAbstract ability)
    {
        if(currentAbility != null) currentAbility.Deactivate();
        currentAbility = ability;
        currentAbility.Activate();
    }
}
