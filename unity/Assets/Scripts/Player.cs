using UnityEngine;
using SimpleJSON;
using System;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	public GetGameData GameDataScript;
	public GoodieParams GoodieDataScript;


	//texte
	public Text MessageText;

	private string pickUpUrl = "https://retrohunter-987.appspot.com/pickup";
	private string storeUrl = "https://retrohunter-987.appspot.com/store";
	//private string url = "http://localhost:15080/pickup";	
	public string playername;
	public string playercode;
	public bool hasItem = false;
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

	//public string hasitemid ;
	public int hasitemtype ;

	// Use this for initialization
	void Start () {
		playername = PlayerPrefs.GetString("playername");
		playercode = PlayerPrefs.GetString("playercode");
	}

	void OnTriggerEnter(Collider other) {
		// pickup item
		if (other.gameObject.tag == "Goodie" && !hasItem && other.gameObject.GetComponent<GoodieParams>().takenBy == "None") {
			hasItem = true;
			ItemId = other.gameObject.GetComponent<GoodieParams>().id;
			ItemType = other.gameObject.GetComponent<GoodieParams>().type;
			Debug.Log("Valid Hit at :" + ItemId + " " + ItemType.ToString());
			MessageText.text = "ITEM PICKED\n##GO to the\nPIXEL PIT##";
			StartCoroutine(PickupItem());
		}

		if (other.gameObject.tag == "Goodie" && other.gameObject.GetComponent<GoodieParams>().takenBy != "None" && other.gameObject.GetComponent<GoodieParams>().takenBy != playername) {
			Debug.Log("Allready taken by:" + other.gameObject.GetComponent<GoodieParams>().takenBy);
		}


		//drop item
		if (other.gameObject.tag == "Pit" && hasItem) {
			print("pit");
			StartCoroutine(StoreItem());
		}
	}
	
	private IEnumerator PickupItem() {
		WWWForm form = new WWWForm();
		form.AddField("name", playername);
		form.AddField("usercode", playercode);
		form.AddField("geopos", transform.position.z+","+transform.position.x);
		form.AddField("heading", Input.compass.trueHeading.ToString("R"));
		form.AddField("itemid", ItemId);
        form.AddField("itemtype", ItemType);
		WWW pickupResponse = new WWW(pickUpUrl, form);
		
		yield return pickupResponse;
		
        if (pickupResponse.error == null) {
			var N = JSON.Parse(pickupResponse.text);
			string Status = N[0]["status"].ToString().Replace("\"", "");
			string itemID = N[0]["item"].ToString().Replace("\"", "");

			//fehler bei pick up
			if (Status != "item picked") {
				hasItem = false;
				ItemId = "";
				ItemType = 0;
				MessageText.text = "PICKUP FAILED\n#ITEM OWNED BY\nOTHER PLAYER#";
			}

		} else {	
			Debug.Log("Error: "+ pickupResponse.error);
		}
	}

	private IEnumerator StoreItem() {
		WWWForm form = new WWWForm();
		form.AddField("name", playername);
		form.AddField("usercode", playercode);
		form.AddField("geopos", transform.position.z+","+transform.position.x);
		form.AddField("heading", Input.compass.trueHeading.ToString("R"));
		form.AddField("itemid", ItemId);
		form.AddField("itemtype", ItemType);
		WWW storeResponse = new WWW(storeUrl, form);
		
		yield return storeResponse;
		
		if (storeResponse.error == null) {
			var N = JSON.Parse(storeResponse.text);
			string Status = N[0]["status"].ToString().Replace("\"", "");
			string itemID = N[0]["item"].ToString().Replace("\"", "");

			//kein fehler bei store
			if (Status == "item stored") {
				hasItem = false;
				ItemId = "" ;
				MessageText.text = "ITEM SUCCESSFULLY \n#DROPPED#";
				
				
			}
			
		} else {	
			Debug.Log("Error: "+ storeResponse.error);
		}
	}	
}
