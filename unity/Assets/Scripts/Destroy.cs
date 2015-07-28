using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {


	public float time = 5;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, time+Random.value);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
