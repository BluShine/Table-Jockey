using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//true if there is free space either in front or behind the object
public class SlideArrangement : Arrangement
{
    public Vector3 boxDimensions;
    public Vector3 boxOffset;
    public float pushForward = 1f;
    public float pushBackwards = 1f;

    public override bool evaluate()
    {
        Collider[] frontCols = Physics.OverlapBox(transform.position + transform.rotation * boxOffset + transform.forward * pushForward,
            boxDimensions * .49f, transform.rotation);
        Collider[] rearCols = Physics.OverlapBox(transform.position + transform.rotation * boxOffset - transform.forward * pushForward,
            boxDimensions * .49f, transform.rotation);
        //remove collisions with ourself.
        Collider fCol = null;
        Collider rCol = null;
        foreach(Collider c in frontCols)
        {
            Furniture f = c.GetComponentInParent<Furniture>();
            if (f == null || f != furnitureParent)
            {
                fCol = c;
            }
        }
        foreach (Collider c in rearCols)
        {
            Furniture f = c.GetComponentInParent<Furniture>();
            if (f == null || f != furnitureParent)
            {
                rCol = c;
            }
        }
        //see if we had any collisions
        if (rCol != null && fCol != null)
        {
            List<Vector3> fails = new List<Vector3>();;
            fails.Add(transform.position + transform.forward * (pushBackwards + boxDimensions.z) + Vector3.up * .1f);
            fails.Add(transform.position - transform.forward * (pushForward + boxDimensions.z) + Vector3.up * .1f);
            furnitureParent.failurePos = fails;
            return false;
        }
        return true;
    }
}
