
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] int damage = 1;
    [SerializeField] float rotationSpeed = 15f;
    [SerializeField] Transform saw;
    [SerializeField] Transform waipoints;
    [SerializeField] AudioClip audioClip;
    private int index;
    private Vector3 currentTargetPosition;
    private bool isMoving;
    private void Start() {
        if(waipoints.childCount > 1) 
        {
            currentTargetPosition = waipoints.GetChild(index).position;
            transform.right = (waipoints.GetChild(index).position - saw.transform.position).normalized;
            isMoving = true;
            return;
        }
        
    }
    void Update()
    {
        saw.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
        if(!isMoving) return;
        saw.transform.position = Vector3.MoveTowards(saw.transform.position, currentTargetPosition, moveSpeed * Time.deltaTime);
        if(saw.transform.position == currentTargetPosition) GetNextTargetPosition();
    }

    private void GetNextTargetPosition()
    {
        index++;
        if(index >= waipoints.childCount) index = 0;
        //saw.transform.right = (waipoints.GetChild(index).position - saw.transform.position).normalized;
        currentTargetPosition = waipoints.GetChild(index).position;
    }

    public int GetDamageValue()
    {
        return damage;
    }
    
}
