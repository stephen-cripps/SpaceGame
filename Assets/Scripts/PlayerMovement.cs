using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody rb;
    public float rotStr;
    public float thrustStr;
    public float dragStr;
    float rot;
    float thrust;
    Vector3 thrustDir;
    Vector3 initDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rot = 0;
        initDir.Set(0, 0, 90);
    }

    private void Update()
    {
        rot = Input.GetAxis("Horizontal");
        thrust = Input.GetAxis("Vertical");
        thrustDir = transform.up;
        Debug.Log(transform.up);
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.AngleAxis(rot * rotStr, transform.right) * transform.rotation;
        //rb.velocity = thrustDir * thrust * thrustStr;
        rb.AddForce(thrustDir * thrust * thrustStr);
        //apply planar drag for control
        //TODO Change plane to be plane of player or everything gets fucked at angles 

        AddDrag();
    }

    private void AddDrag()
    {
        Vector3 v = rb.velocity;
        v.x = rb.velocity.x * (1 / dragStr);
        v.z = rb.velocity.z * (1 / dragStr);
        rb.velocity = v;

    }
}
