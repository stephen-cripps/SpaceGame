using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagForce : MonoBehaviour {

    Rigidbody rb;
    public float magStr;
    public float mainThrustMod; //allows you to give extra strength to main mag thruster
    float fm;
    float l = 1F; // limit used to make sure the upward force does not go to inf
    

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        // Should convert the scale values here to draw directly from rb 
        VerticalThrusterAtPos(3*transform.up);
        VerticalThrusterAtPos(-3*transform.up);
        VerticalThrusterAtPos(1*transform.forward);
        VerticalThrusterAtPos(-1*transform.forward);
        //FullVerticalThruster();
    }

    private void FullVerticalThruster()
    {
        fm = CalculateMag(new Vector3(0, 0, 0));
        rb.AddForce(0, fm*mainThrustMod, 0);
    }
    

    private void VerticalThrusterAtPos(Vector3 position)
    {
       fm = CalculateMag(position)/10;
       rb.AddForceAtPosition(new Vector3(0, fm, 0), transform.position + position);
    }


    private float CalculateMag(Vector3 position)
    {
        float y = CalculateHeight(position);
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

    private float CalculateHeight(Vector3 pos)
    {
        var direction = -transform.right;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + pos, direction, out hit))
        {
           // Debug.DrawRay(transform.position + pos, direction *10 ,Color.green,1,false);
          
            var height = hit.distance;       
            return height;
        }
        else
        {
            return 100;
        }
       

    }
}
