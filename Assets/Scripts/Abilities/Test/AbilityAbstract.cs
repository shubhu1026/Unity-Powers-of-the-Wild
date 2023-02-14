using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class AbilityAbstract : MonoBehaviour
{
    [SerializeField] private string nameAbility;
    public float activeTime;
    public float cooldownTime;
    public KeyCode abilityKey; 
    public virtual void Activate()
    {

    }
    public virtual void Deactivate()
    {

    }
    public virtual void ResetAbilityChanges()
    {

    }

}
