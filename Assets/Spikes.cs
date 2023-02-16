using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDamageable
{
    int GetDamageValue();
}
public class Spikes : MonoBehaviour, IDamageable
{
    private enum State
    {
        ready,
        active,
        reload
    }
    [SerializeField] int damage = 1;
    [SerializeField] float activeSpeed = 10f;
    private State state;
    private Vector3 movement = new Vector3(0, 0.8f, 0);
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float delayTime;
    private float counter;
    
    private void Start() {
        endPosition = transform.position ;
        startPosition = transform.position - movement;
        transform.position = startPosition;
    }
    void Update()
    {
        switch (state)
        {
            default:
            case State.ready : 
            break;
            case State.active : 
            {
                
                counter += Time.deltaTime;
                if(counter < delayTime)return;
                transform.position = Vector3.MoveTowards(transform.position, endPosition, activeSpeed * Time.deltaTime);
                if(transform.position != endPosition) return;                
                counter = 0;
                state = State.reload;
                Debug.Log("reload state");
            }
            break;
            case State.reload :
            {
                counter += Time.deltaTime;
                if(counter < delayTime)return;
                transform.position = Vector3.MoveTowards(transform.position, startPosition, activeSpeed * Time.deltaTime);
                if(transform.position != startPosition)return;              
                counter = 0;
                state = State.ready;  
                Debug.Log("ready state");              
            }
            break;
        }
        
    }
    public void ActivateTrap(float time)
    {
        if(state == State.ready)
        {
            Debug.Log("trap was activated");
            counter = 0;
            delayTime = time;
            state = State.active;
        }
    }

    public int GetDamageValue()
    {
        return damage;
    }
}
