using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//checks spaces to the left, right, forward, and back for at least one object of the specified furniture type.
public class FurnitureCastArrangement : Arrangement
{
    public Vector3 boxDimensions;
    public Vector3 boxOffset;
    public float pushAmount = 1f;

    public Furniture.FurnitureType furnitureType;

    public override bool evaluate()
    {
        Furniture foundFurniutre = null;
        for(int i = 0; i < 4; i++)
        {
            //pick direction
            Vector3 pushDir = Vector3.zero;
            switch(i)
            {
                case 0:
                    pushDir = transform.forward * pushAmount;
                    break;
                case 1:
                    pushDir = -transform.forward * pushAmount;
                    break;
                case 2:
                    pushDir = transform.right * pushAmount;
                    break;
                case 3:
                    pushDir = -transform.right * pushAmount;
                    break;
            }
            //check for collisions
            Collider[] collisions = Physics.OverlapBox(transform.position + transform.rotation * boxOffset + pushDir,
            boxDimensions * .49f, transform.rotation);
            //see if any of the collisions are the right furniture type and not ourself.
            foreach (Collider c in collisions)
            {
                Furniture f = c.GetComponentInParent<Furniture>();
                if (f != null && f != furnitureParent && f.furnitureType == furnitureType)
                {
                    foundFurniutre = f;
                }
            }
        }
        //did we find a matching piece of furniture?
        if (foundFurniutre != null) {
            return true;
        }
        List<Vector3> fails = new List<Vector3>(); ;
        fails.Add(transform.position);
        fails.Add(transform.position + (pushAmount + boxDimensions.x) * transform.right + Vector3.up * .1f);
        fails.Add(transform.position + Vector3.right * .01f);
        fails.Add(transform.position - (pushAmount + boxDimensions.x) * transform.right + Vector3.up * .1f);
        fails.Add(transform.position);
        fails.Add(transform.position + (pushAmount + boxDimensions.z) * transform.forward + Vector3.up * .1f);
        fails.Add(transform.position + Vector3.left * .01f);
        fails.Add(transform.position - (pushAmount + boxDimensions.z) * transform.forward + Vector3.up * .1f);
        furnitureParent.failurePos = fails;
        return false;
    }
}
