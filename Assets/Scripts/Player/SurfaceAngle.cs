using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceAngle : MonoBehaviour
{
    [SerializeField] Transform rearRayPos;
    [SerializeField] Transform frontRayPos;
    [SerializeField] LayerMask layerMask;

    float surfaceAngle;
    bool uphill;
    bool flatSurface;

    float angle;

    public float GetSurfaceAngle()
    {
        rearRayPos.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);
        frontRayPos.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);

        RaycastHit rearHit;
        if(Physics.Raycast(rearRayPos.position, rearRayPos.TransformDirection(-Vector3.up), out rearHit, 1.2f, layerMask))
        {
            surfaceAngle = Vector3.Angle(rearHit.normal, Vector3.up);
        }
        
        return surfaceAngle;
    }
}
