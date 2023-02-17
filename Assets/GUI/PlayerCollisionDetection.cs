using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    private void OnTriggerStay(Collider other) {
        if(other.TryGetComponent<ITraptable>(out var trap))
        {
            trap.ActivateTrap();
        }        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<IDamageable>(out var damager))
        {
            Debug.Log(gameObject.name.ToString() + " was hit by " + damager + " do " + damager.GetDamageValue());
        }
    }
    private void onCollisionEnter(Collision other) {
        Debug.Log(other + " in in the player, this is player collider");
    }
}
