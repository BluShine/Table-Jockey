using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//true if there is free space either in front or behind the object
public class FurnitureCastArrangement : Arrangement
{
    public List<Vector3> castPoints;
    public List<Vector3> castDirs;
    public Furniture.FurnitureType furnitureType;

    public float sphereRadius = .15f;

    public override bool evaluate()
    {
        for(int i = 0; i < castPoints.Count; i++)
        {
            Ray ray = new Ray(transform.position + transform.rotation * castPoints[i], 
                transform.rotation * castDirs[i]);
            RaycastHit rayHit = new RaycastHit();
            Physics.SphereCast(ray, sphereRadius, out rayHit);
            Furniture furniture = rayHit.transform.GetComponentInParent<Furniture>();
            if(furniture != null)
            {
                if(furniture.furnitureType == furnitureType)
                {
                    return true;
                }
            }
        }
        List<Vector3> failLines = new List<Vector3>();
        failLines.Add(transform.position + transform.rotation * castPoints[0]);
        for(int i = 0; i < castPoints.Count; i ++)
        {
            failLines.Add(transform.position + transform.rotation * castPoints[i] +
                transform.rotation * castDirs[i] * 10);
            failLines.Add(transform.position + transform.rotation * castPoints[i]);
        }

        furnitureParent.failurePos = failLines;

        return false;
    }
}
