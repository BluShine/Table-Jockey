using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSpawner : MonoBehaviour {

    public GameObject prefab;
    public PhysicMaterial physMaterial;

    public float legWidth = .1f;
    public float legHeight = .3f;
    public float width = 1;
    public float length = 2;
    public float platformHeight = .2f;
    public float mattressHeight = .2f;
    public float pillowHeight = .07f;
    public float pillowSpacing = .2f;
    public float pillowDepth = .4f;
    public float headboardHeight = .65f;
    public float footboardHeight = .3f;

    // Use this for initialization
    void Start () {
        spawnBed(transform.position, transform.rotation);
    }

    public GameObject spawnBed(Vector3 position, Quaternion rotation)
    {
        GameObject bed = Instantiate(prefab);
        bed.GetComponent<MeshFilter>().mesh = BedGenerator.Bed(
            legWidth, legHeight, width, length, platformHeight, mattressHeight,
            pillowHeight, pillowSpacing, pillowDepth, headboardHeight, footboardHeight,
            Color.HSVToRGB(Random.value, Random.Range(.5f, 1f), 1),
            Color.HSVToRGB(Random.value, Random.Range(.5f, 1f), 1)).ToMesh();
        GameObject collider = BedGenerator.BedCollider(
            legWidth, legHeight, width, length, platformHeight, mattressHeight,
            pillowHeight, pillowSpacing, pillowDepth, headboardHeight, footboardHeight);
        collider.transform.parent = bed.transform;
        foreach (Collider c in bed.GetComponentsInChildren<Collider>())
        {
            c.material = physMaterial;
        }
        //bed arrangement: beds require space on either the left or right side.
        SlideArrangement slide = bed.AddComponent<SlideArrangement>();
        slide.boxDimensions = new Vector3(width, legHeight + platformHeight + mattressHeight, length / 2);
        slide.boxOffset = Vector3.up * (legHeight + platformHeight + mattressHeight) / 2;
        slide.pushAmount = width / 2;
        slide.horizontal = true;

        bed.transform.position = position;
        bed.transform.rotation = rotation;

        return bed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, width / 2);
        Vector3 upV = transform.up * legHeight;
        Vector3 fV = transform.forward * length / 2;
        Vector3 rV = transform.right * width / 2;
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position + upV + fV + rV, transform.position + upV + fV - rV);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + upV - fV + rV, transform.position + upV - fV - rV);
    }
}
