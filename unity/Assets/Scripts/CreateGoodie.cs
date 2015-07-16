using UnityEngine;
using System.Collections;

public class CreateGoodie : MonoBehaviour {

	public GameObject newGoodie;

	// Use this for initialization
	void Start () {
		NewGoodie();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void NewGoodie(){
		Debug.Log("new Goodie");
		float xPos = Random.Range(-4,4);
		float zPos = Random.Range(-8,8);
		Instantiate(newGoodie, new Vector3(xPos, 0, zPos), Quaternion.identity);
	}
}
