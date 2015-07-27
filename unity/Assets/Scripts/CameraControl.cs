using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Transform Target;
	bool inRange = false;

	float camX;
	float camZ;
	
	public Camera myCam;

	// Update is called once per frame
	void Update () {
		//out of evoke
		if (Target.position.x < -5 || Target.position.x > 5 || Target.position.z < -9 || Target.position.z > 9) inRange = false;
		else inRange = true;
	}
}
