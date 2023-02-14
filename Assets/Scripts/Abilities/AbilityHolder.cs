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
        firstAbility.abilityKey = KeyCode.Alpha1;
        secondAbility.abilityKey = KeyCode.Alpha2;
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
                if(count1 < 1)
                {
                    firstAbility.Activate(gameObject);
                    count1++;
                }
                
                if(Input.GetKeyDown(KeyCode.Alpha2))
                {
                    firstAbility.ResetAbilityChanges(gameObject);
                    count1 = 0;
                    currentAbility = AbilityState.ability2;
                }
                else if(Input.GetKeyDown(KeyCode.Alpha3))
                {
                    firstAbility.ResetAbilityChanges(gameObject);
                    count1 = 0;
                    currentAbility = AbilityState.ability3;
                }
                break;
            case AbilityState.ability2:

                if(count2 < 1)
                {
                    secondAbility.Activate(gameObject);
                    count2++;
                }

                if(Input.GetKeyDown(KeyCode.Alpha1))
                {
                    secondAbility.ResetAbilityChanges(gameObject);
                    count2 = 0;
                    currentAbility = AbilityState.ability1;
                }
                else if(Input.GetKeyDown(KeyCode.Alpha3))
                {
                    secondAbility.ResetAbilityChanges(gameObject);
                    count2 = 0;
                    currentAbility = AbilityState.ability3;
                }
                break;
            case AbilityState.ability3:

                if(count3 < 1)
                {
                    thirdAbility.Activate(gameObject);
                    count3++;
                }
                
                if(Input.GetKeyDown(KeyCode.Alpha1))
                {
                    thirdAbility.ResetAbilityChanges(gameObject);
                    count3 = 0;
                    currentAbility = AbilityState.ability1;
                }
                else if(Input.GetKeyDown(KeyCode.Alpha2))
                {
                    thirdAbility.ResetAbilityChanges(gameObject);
                    count3 = 0;
                    currentAbility = AbilityState.ability2;
                }
                break;
        }
    }
}
