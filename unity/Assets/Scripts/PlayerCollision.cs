using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	public GetGameData GameDataScript;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Goodie"){
			//other.gameObject.GetComponent<GoodieParams>().id;
			GameDataScript.PlayerHitObject(other.gameObject.GetComponent<GoodieParams>().id);
		}
		//print(hit);
	}
}
