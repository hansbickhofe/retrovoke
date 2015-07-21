using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public int speed = 25;
	public Vector3 axis;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(axis, speed*Time.deltaTime); 
	}
}
