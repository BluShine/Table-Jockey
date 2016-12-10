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
        if(Input.GetButtonDown("Fire2"))
        {
            GameObject table = Instantiate(chairPrefab);
            table.GetComponent<MeshFilter>().mesh = TableGenerator.Table(.1f, .6f, 1.2f, .8f, .05f, 
                Color.HSVToRGB(Random.value, Random.Range(.5f, 1f), 1)).ToMesh();
            GameObject collider = TableGenerator.TableCollider(.1f, .6f, 1.2f, .8f, .05f);
            collider.transform.parent = table.transform;
            table.transform.position = transform.position;
            table.transform.rotation = transform.rotation;
            Rigidbody body = table.GetComponent<Rigidbody>();
            body.velocity = transform.forward * 10f;
            body.angularVelocity = 10 * new Vector3(.5f - Random.value, .5f - Random.value, .5f - Random.value);
        }
        if (Input.GetButtonDown("Fire3"))
        {
            GameObject bed = Instantiate(chairPrefab);
            bed.GetComponent<MeshFilter>().mesh = BedGenerator.Bed(.1f, .3f, 1, 2, .2f, .2f, 
                .07f, .2f, .4f, .65f, .3f, 
                Color.HSVToRGB(Random.value, Random.Range(.5f, 1f), 1), 
                Color.HSVToRGB(Random.value, Random.Range(.5f, 1f), 1)).ToMesh();
            GameObject collider = BedGenerator.BedCollider(.1f, .3f, 1, 2, .2f, .3f,
                .07f, .2f, .4f, .65f, .3f);
            collider.transform.parent = bed.transform;
            bed.transform.position = transform.position;
            bed.transform.rotation = transform.rotation;
            Rigidbody body = bed.GetComponent<Rigidbody>();
            body.velocity = transform.forward * 10f;
            body.angularVelocity = 10 * new Vector3(.5f - Random.value, .5f - Random.value, .5f - Random.value);
        }
    }
}
