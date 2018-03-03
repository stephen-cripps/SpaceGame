using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForces : MonoBehaviour
{

    Rigidbody rb;
    public float magStr;
    public float levelStr;
    float fm;
    float l = 1F; // limit used to make sure the upward force does not go to inf


     void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

     void FixedUpdate()
    {
        fm = CalculateMag();
        rb.AddForce(0, fm, 0);
        LevelOut();
        
    }

    private float CalculateMag()
    {
        float y = CalculateHeight();
        if (y > l)
        {
            fm = magStr / Mathf.Pow(y, 2);
        }
        else
        {
            fm = (3F * magStr / Mathf.Pow(l, 2)) - ((2F * magStr * y) / Mathf.Pow(l, 3)); //tangent line of the y>l equation at l
        }
        return fm;
    }

    private float CalculateHeight()
    {
        var direction = -transform.right;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit))
        {
            var height = hit.distance;
            return height;
        }
        else
        {
            return 100; 
        }
        
    }

    private void LevelOut()
    {
        var direction = -Vector3.up;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            transform.rotation = Quaternion.FromToRotation(transform.right, hit.normal) * transform.rotation;
  
            
        }
        else
        {
            //You're Fucked
        }

    }
}
