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
        spawnTable(transform.position, transform.rotation);
    }

    public GameObject spawnTable(Vector3 position, Quaternion rotation)
    {
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

        //table arrangement: table require a nearby chair
        FurnitureCastArrangement cast = table.AddComponent<FurnitureCastArrangement>();
        cast.boxDimensions = new Vector3(tableWidth - legWidth, legHeight + tableHeight, tableDepth - legWidth);
        cast.boxOffset = Vector3.up * (legHeight + tableHeight) / 2;
        cast.pushAmount = .2f;
        cast.furnitureType = Furniture.FurnitureType.chair;

        table.transform.position = position;
        table.transform.rotation = rotation;

        return table;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, Mathf.Min(tableWidth,tableDepth) / 2);
        Vector3 upV = transform.up * legHeight;
        Vector3 fV = transform.forward * tableDepth / 2;
        Vector3 rV = transform.right * tableWidth / 2;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + upV + fV + rV, transform.position + upV + fV - rV);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + upV - fV + rV, transform.position + upV - fV - rV);
    }
}
