using ProceduralToolkit.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairSpawner : MonoBehaviour {

    public GameObject prefab;
    public PhysicMaterial physMaterial;

    public float lWidth = .1f;
    public float lHeight = .44f;
    public float sWidth = .51f;
    public float sDepth = .56f;
    public float sHeight = .05f;
    public float bHeight = .39f;

    // Use this for initialization
    void Start () {
        GameObject chair = Instantiate(prefab);
        //mesh
        chair.GetComponent<MeshFilter>().mesh = ChairGenerator.Chair(
            lWidth, lHeight, sWidth, sDepth, sHeight, bHeight, false, false,
            Color.HSVToRGB(Random.value, Random.Range(.5f, 1f), 1)).ToMesh();
        //collider
        GameObject collider = ChairGenerator.ChairCollider(
            lWidth, lHeight, sWidth, sDepth, sHeight, bHeight, false, false);
        collider.transform.parent = chair.transform;
        //physics material
        foreach (Collider c in chair.GetComponentsInChildren<Collider>())
        {
            c.material = physMaterial;
        }
        //chair arrangement: chairs require space either in front or behind the chair.
        SlideArrangement slide = chair.AddComponent<SlideArrangement>();
        slide.boxDimensions = new Vector3(sWidth, lHeight + sHeight + bHeight, sDepth);
        slide.boxOffset = Vector3.up * (lHeight + sHeight + bHeight) / 2;
        slide.pushForward = sDepth;
        slide.pushBackwards = sDepth;

        chair.transform.position = transform.position;
        chair.transform.rotation = transform.rotation;
    }
}
