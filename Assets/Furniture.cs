using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour {

    public enum FurnitureType
    {
        chair, table, bed
    }

    public FurnitureType furnitureType;

    List<Arrangement> arrangements;

    public bool pass = true;

    LineRenderer failureLine;
    public List<Vector3> failurePos;

    public AudioSource hitSound;

    // Use this for initialization
    void Start () {
        failureLine = GetComponent<LineRenderer>();
        arrangements = new List<Arrangement>();
		foreach(Arrangement a in GetComponentsInChildren<Arrangement>())
        {
            arrangements.Add(a);
            a.furnitureParent = this;
        }
        failureLine.enabled = false;
        if (FindObjectOfType<WinDetection>() != null)
        {
            FindObjectOfType<WinDetection>().furnitureList.Add(this);
        }
        if(FindObjectOfType<EndlessMode>() != null)
        {
            FindObjectOfType<EndlessMode>().furnitureList.Add(this);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (hitSound == null)
            return;
        if(collision.relativeVelocity.magnitude > .1f && hitSound.isPlaying == false)
        {
            Pentatonic.PlaySound(hitSound, 7);
        } 
    }
	
	// Update is called once per frame
	void Update () {
        if (Random.value < .05f) //don't need to check every frame, just do it frequently enough.
        {
            pass = true;
            foreach (Arrangement a in arrangements)
            {
                if (a.evaluate() == false)
                    pass = false;
            }

            if (!pass)
            {
                failureLine.enabled = true;
                failureLine.numPositions = failurePos.Count;
                failureLine.SetPositions(failurePos.ToArray());
            }
            else
            {
                failureLine.enabled = false;
            }
        }
	}

    void OnDrawGizmos()
    {
        if (pass)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, .2f);
    }
}
