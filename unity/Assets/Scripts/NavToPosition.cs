using UnityEngine;
using System.Collections;

public class NavToPosition : MonoBehaviour {

	GetLocation myGPS;
	public GameObject DebugTextfield;

	public float lat = 50.944303f;
	public float lon =  6.937723f;
	public float multiX = 3750;
	public float multiY = 7930;

	float posX;
	float posZ;

	int count = 0;

	public GameObject player;
	Vector3 playerRotation;

	// Use this for initialization
	void Start () {
		myGPS = GetComponent<GetLocation>();
	}
	
	// Update is called once per frame
	void Update () {
		if (myGPS.gpsReady){
			count++;
			posX = (Input.location.lastData.longitude - lon)*4500;
			posZ = (Input.location.lastData.latitude - lat)*7000;

			player.transform.position = new Vector3(posX,0,posZ);

			playerRotation = new Vector3(0,Input.compass.trueHeading,0);
			player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(playerRotation), Time.deltaTime * 1.0f); 
			//player.transform.rotation = Quaternion.Euler(0, Input.compass.trueHeading, 0);
	

		} else {
			count++;
			posX = (6.937723f - lon)*4500;
			posZ = (50.944303f - lat)*7000;

			// mitte: 50.944303 - 6.937723
			// oben rechts: 50.945627 - 6.938738
			// unten rechts: 50.943015 - 6.938892
			
			player.transform.position = new Vector3(posX,0,posZ);
		}

		DebugTextfield.GetComponent<TextMesh>().text = count+" "+Input.compass.trueHeading+"\n"+posX+" "+posZ;
	}
}
