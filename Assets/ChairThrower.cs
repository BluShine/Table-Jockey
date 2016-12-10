using ProceduralToolkit.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairThrower : MonoBehaviour {

    public GameObject chairPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            GameObject chair = Instantiate(chairPrefab);
            chair.GetComponent<MeshFilter>().mesh = ChairGenerator.Chair(.1f, .44f, .51f, .56f, .05f, .39f, false, false,
                Color.HSVToRGB(Random.value, Random.Range(.5f, 1f), 1)).ToMesh();
            GameObject collider = ChairGenerator.ChairCollider(.1f, .44f, .51f, .56f, .05f, .39f, false, false);
            collider.transform.parent = chair.transform;
            chair.transform.position = transform.position;
            chair.transform.rotation = transform.rotation;
            Rigidbody body = chair.GetComponent<Rigidbody>();
            body.velocity = transform.forward * 10f;
            body.angularVelocity = 10 * new Vector3(.5f - Random.value, .5f - Random.value, .5f - Random.value);
        }
	}
}
