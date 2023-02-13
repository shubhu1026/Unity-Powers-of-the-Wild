using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] Ability ability1;
    [SerializeField] Ability ability2;
    [SerializeField] Ability ability3;

    float activeTime;
    float cooldownTime;
    
    enum AbilityState{
        ready,
        active,
        cooldown
    }

    AbilityState state = AbilityState.ready;

    [SerializeField] bool abilityButtonPressed;

    void Start() 
    {
        ability1.abilityKey = KeyCode.Alpha1;
        ability2.abilityKey = KeyCode.Alpha2;
        ability3.abilityKey = KeyCode.Alpha3;
    }

    void Update()
    {
        SwitchAbilityState(ability1);
        SwitchAbilityState(ability2);
        SwitchAbilityState(ability3);
    }

    void SwitchAbilityState(Ability ability)
    {
        switch(state)
        {
            case AbilityState.ready:
                if(Input.GetKeyDown(ability.abilityKey))
                {
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                }
                break;
            case AbilityState.active:
                if(activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability.ResetAbilityChanges(gameObject);
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if(cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }


}
