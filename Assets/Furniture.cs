﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour {

    public enum FurnitureType
    {
        chair, table, bed
    }

    public FurnitureType furnitureType;

    List<Arrangement> arrangements;

    bool pass;

    LineRenderer failureLine;
    public List<Vector3> failurePos;

    // Use this for initialization
    void Start () {
        failureLine = GetComponent<LineRenderer>();
        arrangements = new List<Arrangement>();
		foreach(Arrangement a in GetComponentsInChildren<Arrangement>())
        {
            arrangements.Add(a);
            a.furnitureParent = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        pass = true;
		foreach(Arrangement a in arrangements)
        {
            if (a.evaluate() == false)
                pass = false;
        }

        if(!pass)
        {
            failureLine.enabled = true;
            failureLine.numPositions = failurePos.Count;
            failureLine.SetPositions(failurePos.ToArray());
        } else
        {
            failureLine.enabled = false;
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
