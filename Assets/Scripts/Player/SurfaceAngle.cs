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
        else
        {
            uphill = false;
        }

        RaycastHit frontHit;
        Vector3 frontRayStartPos = new Vector3(frontRayPos.position.x, rearRayPos.position.y, frontRayPos.position.z);
        if(Physics.Raycast(frontRayStartPos, frontRayPos.TransformDirection(-Vector3.up), out frontHit, 1.2f, layerMask))
        {
            
        }
        else
        {
            uphill = true;
            flatSurface = false;
        }

        if(frontHit.distance < rearHit.distance)
        {
            uphill = true;
        }
        else if(frontHit.distance > rearHit.distance)
        {
            uphill = false;
            flatSurface = false;
        }
        else if(frontHit.distance == rearHit.distance)
        {
            flatSurface = true;
        }

        return surfaceAngle;
    }
}
