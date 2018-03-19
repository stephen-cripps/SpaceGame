using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody rb;
    public float rotStr; //Strength of rotation
    public float thrustStr; //Strength of thrust in plane
    public float vThrustStr; //strength of thrust out of plane
    public float dragStr; //strength of draf in plane
    float rot;
    float thrust;
    Vector3 thrustDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rot = 0;
    }

    private void Update()
    {
        rot = Input.GetAxis("Horizontal");
        thrust = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        //Rotate
        transform.rotation = Quaternion.AngleAxis(rot * rotStr, transform.right) * transform.rotation;
        // Planar thrust
        thrustDir = GetPThrustDir();
        rb.AddForce(thrustDir * thrust * thrustStr);
        //apply planar drag for control
        AddDrag();
        //Vertical thrust 
        thrustDir = GetVThrustDir();
        rb.AddForce(thrustDir * thrust * vThrustStr);

    }

    private void AddDrag()
    {
        Vector3 v = rb.velocity;
        v.x = rb.velocity.x * (1 / dragStr);
        v.z = rb.velocity.z * (1 / dragStr);
        rb.velocity = v;
    }
    private Vector3 GetPThrustDir()
    { 
        thrustDir = transform.up;
        thrustDir.y = 0;
        return thrustDir;
    }
    private Vector3 GetVThrustDir()
    {
        thrustDir = transform.up;
        thrustDir.x = 0;
        thrustDir.z = 0;
        return thrustDir;
    }
}

