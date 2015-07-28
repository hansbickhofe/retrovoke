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
		if (Target.position.x < -12 || Target.position.x > 12 || Target.position.z < -16 || Target.position.z > 16) inRange = false;
		else inRange = true;
	}
}
