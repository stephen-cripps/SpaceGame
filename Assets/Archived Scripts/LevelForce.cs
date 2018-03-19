using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelForce : MonoBehaviour {
	public float levelStr;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        LevelOut();
	}

	private void LevelOut()
	{
		var direction = -Vector3.up;
		RaycastHit hit;
		Debug.DrawRay(transform.position, -Vector3.up*10 ,Color.blue,1,false );
		if (Physics.Raycast(transform.position, direction, out hit))
		{
			transform.rotation = Quaternion.FromToRotation(transform.right, hit.normal) * transform.rotation;
            
            // transform.rotation = Quaternion.Slerp(Quaternion.Euler(transform.right), Quaternion.Euler(hit.normal), 0.001f) * transform.rotation;

            Debug.DrawRay(transform.position, transform.forward * 10, Color.green, 1, false);
            Debug.DrawRay(transform.position, hit.normal * 10, Color.red, 1, false);

        }
		else
		{
			//this shouldn't happen so just reset
			Quaternion Rot = transform.rotation;
			Rot.x = 0;
			Rot.z = 90;
			transform.rotation = Rot;
  
		}

	}
}
