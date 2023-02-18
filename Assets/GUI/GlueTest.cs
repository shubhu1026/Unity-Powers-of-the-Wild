using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueTest : MonoBehaviour
{
    public bool isSticky;    
    
    private void OnCollisionEnter(Collision other) {
        if(!isSticky)
        {
            return;
            // var joint = GetComponent<FixedJoint>();
            // if(joint == null)
            // {
            //     return;
            // }
            // else
            // {
            //     joint.connectedBody = null;
            // }
        }
        var v = other.gameObject.GetComponent<GlueTest>();
        if(v == null) return;

        gameObject.GetComponent<Rigidbody>().mass = 0.5f;
        var fixedJoint = gameObject.AddComponent<FixedJoint>();
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        Debug.Log(rb);
        fixedJoint.connectedBody = rb;
        fixedJoint.breakForce = float.PositiveInfinity;        
    }
}
