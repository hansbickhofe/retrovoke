using UnityEngine;
using System.Collections;

public class GoodieParams : MonoBehaviour {


	public int speed = 25;
	public Vector3 axis;

	public string id = "";
	public string takenBy = "";
	public int type ;
	public bool posFromPlayer = false;
	
	public GameObject iconText;
	//public GameObject iconFaction;
	
	public GameObject playerPosition;

	// Use this for initialization
	void Start () {
		playerPosition = GameObject.Find("PlayerObj");
	}

	void Update () {
		if (posFromPlayer){
			transform.position = playerPosition.transform.position + new Vector3(0,0,.75f);
			transform.localEulerAngles = new Vector3(90,0,0);
		} else {
			transform.Rotate(axis, speed*Time.deltaTime);
		}
	}


	public void showIcon (string name) {
		iconText.GetComponent<TextMesh>().text = name;
//		if (faction == "1") iconFaction.GetComponent<TextMesh>().text = "INV";
//		else if (faction == "2") iconFaction.GetComponent<TextMesh>().text = "PAC";
//		else if (faction == "3") iconFaction.GetComponent<TextMesh>().text = "GAL";
	}
}
