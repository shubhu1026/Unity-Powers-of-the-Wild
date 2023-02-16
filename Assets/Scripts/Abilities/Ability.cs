using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ability : ScriptableObject
{
    [SerializeField] new string name;
    public float activeTime;
    public float cooldownTime;

    public bool isActive = false;

    public KeyCode abilityKey;
    
    public virtual void Activate(GameObject parent) {}
    public virtual void Active(GameObject parent) {}
    public virtual void ResetAbilityChanges(GameObject parent) {}
}
