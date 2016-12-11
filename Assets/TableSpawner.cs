using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSpawner : MonoBehaviour {

    public GameObject prefab;
    public PhysicMaterial physMaterial;

    public float legWidth = .1f;
    public float legHeight = .6f;
    public float tableWidth = 1.2f;
    public float tableDepth = .8f;
    public float tableHeight = .05f;

	// Use this for initialization
	void Start () {
        GameObject table = Instantiate(prefab);
        table.GetComponent<MeshFilter>().mesh = TableGenerator.Table(
            legWidth, legHeight, tableWidth, tableDepth, tableHeight,
            Color.HSVToRGB(Random.value, Random.Range(.5f, 1f), 1)).ToMesh();
        GameObject collider = TableGenerator.TableCollider(
            legWidth, legHeight, tableWidth, tableDepth, tableHeight);
        collider.transform.parent = table.transform;
        foreach (Collider c in table.GetComponentsInChildren<Collider>())
        {
            c.material = physMaterial;
        }

        table.transform.position = transform.position;
        table.transform.rotation = transform.rotation;
    }
}
