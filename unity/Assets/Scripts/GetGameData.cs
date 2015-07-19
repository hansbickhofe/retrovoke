using UnityEngine;
using System.Collections;

public class GetGameData : MonoBehaviour {

	public GameObject[] allGoodies;

	// Use this for initialization
	void Start () {
		CreateNewGoodie();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateNewGoodie(){
		Debug.Log("new Goodie");
		float xPos = Random.Range(-4,4);
		float zPos = Random.Range(-8,8);
		//Instantiate(newGoodie, new Vector3(xPos, 0, zPos), Quaternion.identity);
	}
}
