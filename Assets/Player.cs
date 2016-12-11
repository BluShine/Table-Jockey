using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float pushForce = 10f;

    Rigidbody inhabitedObject;

    SmoothMouseLook mLook;

	// Use this for initialization
	void Start () {
        mLook = GetComponent<SmoothMouseLook>();
	}
	
	// Update is called once per frame
	void Update () {
        if (inhabitedObject == null)
        {
            mLook.enabled = true;
            if (Input.GetButtonDown("Fire1"))
            {
                clickFurniture();
            }
        } else
        {
            mLook.enabled = false;
            transform.position = inhabitedObject.transform.position + Vector3.up * 5f;
            transform.rotation = inhabitedObject.transform.rotation * Quaternion.Euler(90, 0, 0);

            //move
            Vector3 moveForce = Vector3.zero;
            moveForce += Input.GetAxis("Vertical") * inhabitedObject.transform.forward;
            moveForce += Input.GetAxis("Horizontal") * inhabitedObject.transform.right;
            inhabitedObject.AddForce(moveForce * pushForce, ForceMode.Acceleration);
            //rotate
            inhabitedObject.AddTorque(new Vector3(0, Input.GetAxis("Rotate"), 0) * pushForce * 2, ForceMode.Acceleration);

            //keep it from flipping
            inhabitedObject.transform.rotation = Quaternion.Euler(0, inhabitedObject.transform.rotation.eulerAngles.y, 0);

            if (Input.GetButtonDown("Fire1"))
            {
                inhabitedObject = null;
            }
        }
    }

    void clickFurniture()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit rayHit = new RaycastHit();
        if (Physics.Raycast(ray, out rayHit))
        {
            Furniture furnishing = rayHit.collider.transform.GetComponent<Furniture>();
            if (furnishing == null)
            {
                furnishing = rayHit.collider.transform.GetComponentInParent<Furniture>();
            }
            if (furnishing != null)
            {
                inhabitedObject = furnishing.GetComponent<Rigidbody>();
                mLook.enabled = false;
            }
        }
    }

    void rightFurniture()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit rayHit = new RaycastHit();
        if (Physics.Raycast(ray, out rayHit))
        {
            Furniture furnishing = rayHit.collider.transform.GetComponent<Furniture>();
            if (furnishing == null)
            {
                furnishing = rayHit.collider.transform.GetComponentInParent<Furniture>();
            }
            if (furnishing != null)
            {
                furnishing.transform.rotation = Quaternion.Euler(0, 0, 0);
                furnishing.transform.position.Scale(new Vector3(1, 0, 1));
            }
        }
    }

    void pushFurniture(Vector3 force)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit rayHit = new RaycastHit();
        if (Physics.Raycast(ray, out rayHit))
        {
            Furniture furnishing = rayHit.collider.transform.GetComponent<Furniture>();
            if(furnishing == null)
            {
                furnishing = rayHit.collider.transform.GetComponentInParent<Furniture>();
            }
            if (furnishing != null)
            {
                Rigidbody body = furnishing.GetComponent<Rigidbody>();
                body.AddForceAtPosition(force, rayHit.point);
            }
        }
    }
}
