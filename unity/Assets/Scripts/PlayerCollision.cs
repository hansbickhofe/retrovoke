using UnityEngine;
using SimpleJSON;
using System;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	public GetGameData GameDataScript;
	private string url = "https://retrohunter-987.appspot.com/pickup";
	//private string url = "http://localhost:15080/pickup";	
	public string playername;
	public string playerhasitemid;
	public string playercode;
	public string ItemId;
	public int ItemType;
	public float posX;
	public float posZ;
	GetLocation myGPS;
	public float lat = 50.944303f;
	public float lon =  6.937723f;
	public float multiX = 4500;
	public float multiY = 7000;
	Vector3 playerRotation;
	float shipDir;
	public float demoSpeed;
	

	// Use this for initialization
	void Start () {
	playername = PlayerPrefs.GetString("playername");
	playercode = PlayerPrefs.GetString("playercode");
	myGPS = GetComponent<GetLocation>();
	
	if (myGPS.gpsReady){
		posX = (Input.location.lastData.longitude - lon)*multiX;
		posZ = (Input.location.lastData.latitude - lat)*multiY;
	} 
	else 
	{
		
		playerRotation = new Vector3(0,0,0);
		
			if (Input.GetKey(KeyCode.LeftArrow)) {
				posX -= demoSpeed;
				shipDir = -90;
			}
			
			if (Input.GetKey(KeyCode.RightArrow)) {
				posX += demoSpeed;
				shipDir = 90;
			}
			
			if (Input.GetKey(KeyCode.UpArrow)) {
				posZ += demoSpeed;
				shipDir = 0;
			}
			
			if (Input.GetKey(KeyCode.DownArrow)) {
				posZ -= demoSpeed;
				shipDir = 180;
			}		
		}
	}	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Goodie") 
			playerhasitemid = other.gameObject.GetComponent<GoodieParams>().takenBy;
		 {
		 	if (other.gameObject.GetComponent<GoodieParams>().takenBy == "None")
			 {
				if (other.gameObject.GetComponent<PlayerParams>().hasitemid == "")
			 	{
					//other.gameObject.GetComponent<GoodieParams>().id;
					ItemId = other.gameObject.GetComponent<GoodieParams>().id;
					ItemType = other.gameObject.GetComponent<GoodieParams>().type;
					Debug.Log("Valid Hit at :" + ItemId + " " + ItemType.ToString());
					StartCoroutine(PickupItem());
				}
				else 
				{
					Debug.Log("Allready carring Itemd with ID  :" + other.gameObject.GetComponent<PlayerParams>().hasitemid.ToString());
				}
			 }
			 else
			 {
				Debug.Log("Hit other Player: " + other.gameObject.GetComponent<GoodieParams>().takenBy);
			 }
		}
		//print(hit);
	}
	
	
	private IEnumerator PickupItem()
	{
			WWWForm form = new WWWForm();
			form.AddField("name", playername);
			form.AddField("usercode", playercode);
			form.AddField("geopos", posZ+","+posX);
			form.AddField("heading", Input.compass.trueHeading.ToString("R"));
			form.AddField("itemid", ItemId);
            form.AddField("itemtype", ItemType);
            WWW pickupResponse = new WWW(url, form);
			
			yield return pickupResponse;
			
		              if (pickupResponse.error == null) {
			var N = JSON.Parse(pickupResponse.text);
			} else {
				//				Debug.Log("Error: "+ sendPosition.error);
			}
			
	}	
}
