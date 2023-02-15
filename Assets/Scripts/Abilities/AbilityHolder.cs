using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] Ability firstAbility;
    [SerializeField] Ability secondAbility;
    [SerializeField] Ability thirdAbility;

    // Ability activeAbility;
    // Ability previousAbility;

    int count1 = 0;
    int count2 = 0;
    int count3 = 0;

    enum AbilityState{
        none,
        ability1,
        ability2,
        ability3
    }

    AbilityState currentAbility = AbilityState.none;

    // Start is called before the first frame update
    void Start()
    {
        if(firstAbility != null)
        firstAbility.abilityKey = KeyCode.Alpha1;
        if(secondAbility != null)
        secondAbility.abilityKey = KeyCode.Alpha2;
        if(thirdAbility != null)
        thirdAbility.abilityKey = KeyCode.Alpha3;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAbilityState();
    }

    void SwitchAbilityState()
    {
        switch(currentAbility)
        {
            case AbilityState.none:
                if(Input.GetKeyDown(KeyCode.Alpha1))
                {
                    currentAbility = AbilityState.ability1;
                }
                else if(Input.GetKeyDown(KeyCode.Alpha2))
                {
                    currentAbility = AbilityState.ability2;
                }
                else if(Input.GetKeyDown(KeyCode.Alpha3))
                {
                    currentAbility = AbilityState.ability3;
                }
                break;
            case AbilityState.ability1:
                AbilityStatus(firstAbility, ref count1);
                break;
            case AbilityState.ability2:
                AbilityStatus(secondAbility, ref count2);
                break;
            case AbilityState.ability3:
                AbilityStatus(thirdAbility, ref count3);
                break;
        }
    }

    void AbilityStatus(Ability ability, ref int count)
    {
        if(count < 1)
        {
            ability.Activate(gameObject);
            count++;
        }

        if(Input.GetKeyDown(ability.abilityKey))
        {
            ability.ResetAbilityChanges(gameObject);
            count = 0;
            currentAbility = AbilityState.none;
        }
        else
        {
           if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                ability.ResetAbilityChanges(gameObject);
                count = 0;
                currentAbility = AbilityState.ability1;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                ability.ResetAbilityChanges(gameObject);
                count = 0;
                currentAbility = AbilityState.ability2;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                ability.ResetAbilityChanges(gameObject);
                count = 0;
                currentAbility = AbilityState.ability3;
            }
        }
    }
}
