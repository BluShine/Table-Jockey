using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float pushForce = 10f;
    public float upRightForce = 100f;

    Rigidbody inhabitedObject;

    SmoothMouseLook mLook;

    Vector3 velocity = Vector3.zero;
    public float maxSpeed = 2f;
    public float acceleration = 1f;
    public float drag = 1f;

    WinDetection winDetect;

    public AudioSource clickSound;
    public AudioSource unclickSound;
    public AudioSource moveSound;
    public AudioSource rotateSound;

	// Use this for initialization
	void Start () {
        mLook = GetComponent<SmoothMouseLook>();
        winDetect = FindObjectOfType<WinDetection>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

        if (inhabitedObject == null)
        {
            mLook.enabled = true;
            if (Input.GetButtonDown("Fire1"))
            {
                clickFurniture();
                clickSound.Play();
            }
            //camera movement
            Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (moveVector.magnitude > 0)
            {
                //movement
                velocity += moveVector.normalized * acceleration * Time.deltaTime;
            } else
            {
                //drag
                if(velocity.magnitude < drag * Time.deltaTime)
                {
                    velocity = Vector3.zero;
                } else
                {
                    velocity -= velocity * drag * Time.deltaTime;
                }
            }
            //limit max speed
            if(velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
            }
            transform.position += transform.rotation * velocity * Time.deltaTime;
        } else
        {
            mLook.rotationY = -90;
            mLook.enabled = false;
            //point the camera down
            transform.position = inhabitedObject.transform.position + Vector3.up * 3f;
            Quaternion lookDown = inhabitedObject.transform.rotation;
            lookDown = Quaternion.Euler(0, lookDown.eulerAngles.y, 0);//remove roll and tilt.
            lookDown = lookDown * Quaternion.Euler(90, 0, 0);//point downwards.
            transform.rotation = lookDown;

            //move
            Vector3 moveForce = Vector3.zero;
            moveForce += Input.GetAxis("Vertical") * inhabitedObject.transform.forward;
            moveForce += Input.GetAxis("Strafe") * inhabitedObject.transform.right;
            inhabitedObject.AddForce(moveForce * pushForce, ForceMode.Acceleration);
            if(moveForce.magnitude > 0 && Random.value < .1f && !moveSound.isPlaying)
            {
                Pentatonic.PlaySound(moveSound, 4);
            }
            //rotate
            if (inhabitedObject.angularVelocity.magnitude < 1)
            {
                inhabitedObject.AddTorque(new Vector3(0, Input.GetAxis("Horizontal"), 0) * pushForce * 5, ForceMode.Acceleration);
            } else
            {
                inhabitedObject.AddTorque(new Vector3(0, Input.GetAxis("Horizontal"), 0) * pushForce * 2, ForceMode.Acceleration);
            }
            if (Input.GetAxis("Horizontal") != 0 && Random.value < .1f && !rotateSound.isPlaying)
            {
                Pentatonic.PlaySound(rotateSound, 4);
            }
            //keep it from flipping
            Vector3 curRot = inhabitedObject.transform.rotation.eulerAngles;
            float xR = curRot.x;
            if (xR > 180)
                xR -= 360;
            float zR = curRot.z;
            if (zR > 180)
                zR -= 360;
            //Debug.Log("x " + xR + " z " + zR);
            if (Mathf.Abs(xR) > 10 || Mathf.Abs(zR) > 10)
            {
                inhabitedObject.AddTorque(new Vector3(xR, 0, zR) * upRightForce);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                inhabitedObject = null;
                unclickSound.Play();
                if (winDetect != null)
                {
                    winDetect.CheckWin();
                }
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
