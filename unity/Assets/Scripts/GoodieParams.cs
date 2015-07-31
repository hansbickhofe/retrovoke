using UnityEngine;
using System.Collections;

public class GoodieParams : MonoBehaviour {


	public int speed = 25;
	public Vector3 axis;

	public string id = "";
	public string takenBy = "";
	public int type ;
	public bool posFromPlayer = false;

	public GameObject iconGalaga;
	public GameObject iconPacman;
	public GameObject iconInvader;
	public GameObject iconText;
	
	public GameObject playerPosition;

	// Use this for initialization
	void Start () {
		playerPosition = GameObject.Find("PlayerObj");
		iconGalaga.SetActive(false);
		iconPacman.SetActive(false);
		iconInvader.SetActive(false);
	}

	void Update () {
		if (posFromPlayer){
			transform.position = playerPosition.transform.position + new Vector3(0,0,.75f);
			transform.localEulerAngles = new Vector3(90,0,0);
		} else {
			transform.Rotate(axis, speed*Time.deltaTime);
		}
	}
}
