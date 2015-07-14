using UnityEngine;
using System.Collections;

public class NavToPosition : MonoBehaviour {

	GetLocation myGPS;

	public float lat = 50.944303f;
	public float lon =  6.937723f;
	public float multiX = 3750;
	public float multiY = 7930;

	float posX;
	float posZ;

	public GameObject player;

	// Use this for initialization
	void Start () {
		myGPS = GetComponent<GetLocation>();
	}
	
	// Update is called once per frame
	void Update () {
		posX = (myGPS.myLat - lat)*3750;
		posZ = (myGPS.myLon - lon)*7950;

		player.transform.position = new Vector3(posX,0,posZ);
	}
}
