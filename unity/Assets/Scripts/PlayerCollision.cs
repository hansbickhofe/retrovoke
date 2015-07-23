using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	GetGameData GameDataScript;

	// Use this for initialization
	void Start () {
		GameDataScript = GetComponent<GetGameData>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		//GameDataScript.PlayerHitObject (other.gameObject.name);
		print(hit);
	}
}
