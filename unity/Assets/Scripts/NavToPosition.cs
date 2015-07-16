using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NavToPosition : MonoBehaviour {

	GetLocation myGPS;
	public GameObject DebugTextfield;

	public float lat = 50.944303f;
	public float lon =  6.937723f;
	public float multiX = 4500;
	public float multiY = 7000;

	float posX;
	float posZ;

	public GameObject player;
	Vector3 playerRotation;

	public int idleSpeed;
	bool inRange = false;
	public GameObject OutText;

	// Use this for initialization
	void Start () {
		myGPS = GetComponent<GetLocation>();
	}
	
	// Update is called once per frame
	void Update () {
		if (myGPS.gpsReady){
			posX = (Input.location.lastData.longitude - lon)*multiX;
			posZ = (Input.location.lastData.latitude - lat)*multiY;
			playerRotation = new Vector3(0,Input.compass.trueHeading,0);
		} else {
			posX = (6.937723f - lon)*multiX;
			posZ = (50.944303f - lat)*multiY;
			playerRotation = new Vector3(0,0,0);

			//posX = -6; 
			//posZ = -11;

			// mitte: 50.944303 - 6.937723
			// oben rechts: 50.945627 - 6.938738
			// unten rechts: 50.943015 - 6.938892
		}

		if (inRange){
			player.transform.localScale = new Vector3(.5f,.5f,.5f);
			player.transform.position = new Vector3(posX,0,posZ);
			player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(playerRotation), Time.deltaTime * 1.0f);
			OutText.SetActive(false);
		} else {
			outOfEvoke();
		}

		//out of evoke
		if (posX < -5 || posX > 5 || posZ < -9 || posZ > 9) inRange = false;
		else inRange = true; 

		DebugTextfield.GetComponent<Text>().text = inRange+" "+Input.compass.trueHeading+"\n"+posX+" "+posZ;
	}

	void outOfEvoke(){
		OutText.SetActive(true);
		player.transform.localScale = new Vector3(4,4,4);
		player.transform.position = new Vector3(0,5,0);
		player.transform.Rotate(new Vector3(Time.deltaTime * idleSpeed,Time.deltaTime * idleSpeed,0));
	}
}
