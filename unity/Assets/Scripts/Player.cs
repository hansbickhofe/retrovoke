using UnityEngine;
using SimpleJSON;
using System;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	public GetGameData GameDataScript;
	public GoodieParams GoodieDataScript;
	
	//pixelpit
	public Transform PixelPit1;
	public Transform PixelPit2;
	public Transform PixelPit3;

	public GameObject PixelParticle;

	//texte
	public Text MessageText;
	public Text ScoreText;
	public Text TimeText;
	public Text StatsButton;


	private string pickUpUrl = "https://retrohunter-987.appspot.com/pickup";
	private string storeUrl = "https://retrohunter-987.appspot.com/store";
	private string dropUrl = "https://retrohunter-987.appspot.com/drop";
	private string scoreUrl = "https://retrohunter-987.appspot.com/score";

	// private string pickUpUrl = "http://localhost:15080/pickup";
	// private string storeUrl = "http://localhost:15080/store";
	// private string dropUrl = "http://localhost:15080/drop";

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
	public string currentPit;

	public float ItemTime = 30;

	//public string hasitemid ;
	public int hasitemtype ;

	// Use this for initialization
	void Start () {
		// PlayerPrefs.SetString("playername","PED");
		// PlayerPrefs.SetString("playercode","NNYG696505WZ");
		// PlayerPrefs.SetInt("playerteam", 1);
		playername = PlayerPrefs.GetString("playername");
		playercode = PlayerPrefs.GetString("playercode");

		hasItem = false;
		ItemId = "" ;

		ScoreText.text = "SCORE\n"+PlayerPrefs.GetInt("PlayerScore");
	}

	void Update(){

		//mouse click
		if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitClick;
			
			if (Physics.Raycast(ray, out hitClick)){
				Debug.Log ("Touched: "+hitClick.collider.name);
				Click (hitClick.collider.name);
			}
		}
		
		// touch input
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch (0).position)), Vector2.zero);
			
			if (hit.collider != null)
			{
				Debug.Log ("Touched: "+hit.collider.name);
				Click (hit.collider.name);
			}
		}


		if (hasItem){
			StatsButton.text = "";
			if (ItemTime > 0) {
				ItemTime -= Time.deltaTime;
			}
			else {
				StartCoroutine(DropItem());
				hasItem = false; // vorsichtshalber auf false setzen. kann in der coroutine resetted werden, falls der drop schief geht.

				// itemdarstellung refreshen
				GameDataScript.RefreshGameDataOnce();
			}
		} else {
			StatsButton.text = "STATS";
		}

		TimeText.text = "Time\n"+(int)ItemTime;
	}

	void Click(string Target){
		if (Target == "StatsButton") {
			if (!hasItem) Application.LoadLevel("player");
		}
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
			print(other.gameObject.name);
			currentPit = other.gameObject.name;
			StartCoroutine(StoreItem());
		}

//		if (other.gameObject.tag == "Pit") {
//			PixelVolcano();
//		}
	}

	void OnApplicationQuit() {
		print ("quit");
		StartCoroutine(DropItem());
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
			else {
				hasItem = true;
				ItemId = itemID;
				Debug.Log("Pickup: "+ itemID);
				ItemTime = 30;
				GameDataScript.RefreshGameDataOnce();
			}

		} else {	
			Debug.Log("Error: "+ pickupResponse.error);
			hasItem = false;
		}
	}

	private IEnumerator DropItem() {
		WWWForm form = new WWWForm();
		form.AddField("name", playername);
		form.AddField("usercode", playercode);
		form.AddField("geopos", transform.position.z+","+transform.position.x);
		form.AddField("heading", Input.compass.trueHeading.ToString("R"));
		form.AddField("itemid", ItemId);
		form.AddField("itemtype", ItemType);
		WWW dropResponse = new WWW(dropUrl, form);
		hasItem = false; 
		ItemId = "0" ;
		yield return dropResponse;
		
		if (dropResponse.error == null) {
			var N = JSON.Parse(dropResponse.text);
			string Status = N[0]["status"].ToString().Replace("\"", "");
			string itemID = N[0]["item"].ToString().Replace("\"", "");
			
			//kein fehler bei drop
			if (Status == "item dropped") {
				hasItem = false;
				ItemId = "0" ;

				// itemdarstellung refreshen
				GameDataScript.RefreshGameDataOnce();
			}
		} else {

			//reset falls drop schiefgeht
			hasItem = true;
			Debug.Log("Error: "+ dropResponse.error);
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
				MessageText.text = "ITEM SUCCESSFULLY \n#Crunched!#";
				StartCoroutine(GetScore());
				PixelVolcano();
				GameDataScript.RefreshGameDataOnce();
			}
			
		} else {	
			Debug.Log("Error: "+ storeResponse.error);
		}
	}

	private IEnumerator GetScore()
	{
		
		WWWForm form = new WWWForm();
		form.AddField("name", playername);
		
		WWW sendScoreRequest = new WWW(scoreUrl, form);
		
		yield return sendScoreRequest;
		
		if (sendScoreRequest.error == null) {
			var N = JSON.Parse(sendScoreRequest.text);
			PlayerPrefs.SetInt("PlayerScore",int.Parse(N["TotalScore"]));
			ScoreText.text = "SCORE\n"+N["TotalScore"];
			Debug.Log(N);
		} else {
			Debug.Log("Error: "+ sendScoreRequest.error);
		}
	}	

	void PixelVolcano() {

		for (int i=0; i < 50; i++){
			Vector3 initPos = Vector3.zero;

			if (currentPit == "PixelPit1"){
				initPos = PixelPit1.position + UnityEngine.Random.insideUnitSphere * .5f;
			}

			if (currentPit == "PixelPit2"){
				initPos = PixelPit2.position + UnityEngine.Random.insideUnitSphere * .5f;
			}

			if (currentPit == "PixelPit3"){
				initPos = PixelPit3.position + UnityEngine.Random.insideUnitSphere * .5f;
			}

			GameObject newParticle = Instantiate(PixelParticle, initPos, Quaternion.identity) as GameObject;

			//size
			float scale = UnityEngine.Random.value/3;
			newParticle.transform.localScale = new Vector3(scale,scale,scale);

			//color
			Color newColor = new Color(UnityEngine.Random.value*2, UnityEngine.Random.value*2, UnityEngine.Random.value*2, 1.0f );
			newParticle.GetComponent<Renderer>().material.color = newColor;

			//force
			newParticle.GetComponent<Rigidbody>().AddForce(Vector3.up*10);
		}
	}
}
