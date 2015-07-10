using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public int speed = 25;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.down, speed*Time.deltaTime); 
	}
}
