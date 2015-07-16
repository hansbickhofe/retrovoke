using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Transform Target;
	bool inRange = false;

	float camX;
	float camZ;
	
	public Camera myCam;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (inRange){
			camX = Target.position.x;
			if (Target.position.x < -2.5f) camX = -2.5f;
			if (Target.position.x > 2.5f) camX = 2.5f;

			camZ = Target.position.z;
			if (Target.position.z < -4.5f) camZ = -4.5f;
			if (Target.position.z > 4.5f) camZ = 4.5f;

			transform.position = new Vector3(camX,8.5f,camZ);
			myCam.orthographicSize = 5;
		} else {
			myCam.orthographicSize = 9.5f;
		}


		//out of evoke
		if (Target.position.x < -5 || Target.position.x > 5 || Target.position.z < -9 || Target.position.z > 9) inRange = false;
		else inRange = true; 
	}
}
