using ProceduralToolkit.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairThrower : MonoBehaviour {

    public GameObject chairPrefab;

    public PhysicMaterial physMaterial;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 10; i++)
        {
            GameObject thing;
            if(Random.value < .5f)
            {
                thing = makeChair();
            } else if (Random.value < .5f)
            {
                thing = makeTable();
            } else
            {
                thing = makeBed();
            }

            throwThing(thing, new Vector3(Random.Range(-4, 4), 1, Random.Range(-4, 4)),
                new Vector3(.5f - Random.value, .25f - Random.value * .5f, .5f - Random.value), 5);
        }
	}

    GameObject makeChair()
    {
        GameObject chair = Instantiate(chairPrefab);
        chair.GetComponent<MeshFilter>().mesh = ChairGenerator.Chair(.1f, .44f, .51f, .56f, .05f, .39f, false, false,
            Color.HSVToRGB(Random.value, Random.Range(.5f, 1f), 1)).ToMesh();
        GameObject collider = ChairGenerator.ChairCollider(.1f, .44f, .51f, .56f, .05f, .39f, false, false);
        collider.transform.parent = chair.transform;
        foreach(Collider c in chair.GetComponentsInChildren<Collider>())
        {
            c.material = physMaterial;
        }
        
        return chair;
    }

    GameObject makeTable()
    {
        GameObject table = Instantiate(chairPrefab);
        table.GetComponent<MeshFilter>().mesh = TableGenerator.Table(.1f, .6f, 1.2f, .8f, .05f,
            Color.HSVToRGB(Random.value, Random.Range(.5f, 1f), 1)).ToMesh();
        GameObject collider = TableGenerator.TableCollider(.1f, .6f, 1.2f, .8f, .05f);
        collider.transform.parent = table.transform;
        foreach (Collider c in table.GetComponentsInChildren<Collider>())
        {
            c.material = physMaterial;
        }
        
        return table;
    }

    GameObject makeBed()
    {
        GameObject bed = Instantiate(chairPrefab);
        bed.GetComponent<MeshFilter>().mesh = BedGenerator.Bed(.1f, .3f, 1, 2, .2f, .2f,
            .07f, .2f, .4f, .65f, .3f,
            Color.HSVToRGB(Random.value, Random.Range(.5f, 1f), 1),
            Color.HSVToRGB(Random.value, Random.Range(.5f, 1f), 1)).ToMesh();
        GameObject collider = BedGenerator.BedCollider(.1f, .3f, 1, 2, .2f, .3f,
            .07f, .2f, .4f, .65f, .3f);
        collider.transform.parent = bed.transform;
        foreach (Collider c in bed.GetComponentsInChildren<Collider>())
        {
            c.material = physMaterial;
        }
        
        return bed;
    }

    void throwThing(GameObject thing, Vector3 pos, Vector3 dir, float force)
    {
        thing.transform.position = pos;
        thing.transform.rotation = Quaternion.Euler(360 * new Vector3(0, Random.value, 0));
        Rigidbody body = thing.GetComponent<Rigidbody>();
        body.velocity = dir.normalized * force;
        //body.angularVelocity = 10 * new Vector3(.5f - Random.value, .5f - Random.value, .5f - Random.value);
    }
	
	// Update is called once per frame
	void Update () {
        /*
		if(Input.GetButtonDown("Fire1"))
        {
            throwThing(makeChair(), transform.position, transform.forward, 10);
        }
        if(Input.GetButtonDown("Fire2"))
        {
            throwThing(makeTable(), transform.position, transform.forward, 10);
        }
        if (Input.GetButtonDown("Fire3"))
        {
            throwThing(makeBed(), transform.position, transform.forward, 10);
        }*/
    }
}
