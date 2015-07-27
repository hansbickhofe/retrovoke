﻿using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections;

public class NavToPosition : MonoBehaviour {

	GetLocation myGPS;
	GetGameData gameData;


	public GameObject DebugTextfield;

	public float lat = 50.944303f;
	public float lon =  6.937723f;
	public float multiX = 4500;
	public float multiY = 7000;
	public float time = 5;
	private string url = "https://retrohunter-987.appspot.com/pos";
	// private string url = "http://localhost:15080/pos";

	public float posX;
	public float posZ;

	public Transform Cam;
	float camX;
	float camZ;

	float shipDir;

	public GameObject player;
	public GameObject Galaga;
	public GameObject Invader;
	public GameObject Pacman;
	Vector3 playerRotation;

	public int idleSpeed;
	bool inRange = false;

	//texte
	public GameObject OutText;

	public float demoSpeed;
	public string playername;
	public string playercode;
	public int playerteam;

	// Use this for initialization
	void Start () {
		myGPS = GetComponent<GetLocation>();
		gameData = GetComponent<GetGameData>();
		//gameData.CreateNewGoodie();

		posX = (6.937723f - lon)*multiX;
		posZ = (50.944303f - lat)*multiY;
		
		playername = PlayerPrefs.GetString("playername");
		playercode = PlayerPrefs.GetString("playercode");
		playerteam = PlayerPrefs.GetInt("playerteam");
	
		//richtigen playercharcter anzeigen
		Invader.SetActive(false);
		Pacman.SetActive(false);
		Galaga.SetActive(false);
		
		switch (playerteam) 
		{
		case 1:
			Invader.SetActive(true);
			Pacman.SetActive(false);
			Galaga.SetActive(false);			
			break;
		case 2:
			Pacman.SetActive(true);
			Invader.SetActive(false);
			Galaga.SetActive(false);				
			break;
		case 3:
			Pacman.SetActive(false);
			Invader.SetActive(false);		
			Galaga.SetActive(true);
			break;
		default:
			Debug.Log("Incorrect intelligence level.");
			break;	
		}				

		StartCoroutine(SendPos());
	}
	
	// Update is called once per frame
	void Update () {

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


		if (myGPS.gpsReady){
			posX = (Input.location.lastData.longitude - lon)*multiX;
			posZ = (Input.location.lastData.latitude - lat)*multiY;
			shipDir = Input.compass.trueHeading;
		} else {
			 
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


			// camera
			camX = player.transform.position.x;
			if (player.transform.position.x < -2.5f) camX = -2.5f;
			if (player.transform.position.x > 2.5f) camX = 2.5f;
			
			camZ = player.transform.position.z;
			if (player.transform.position.z < -4.5f) camZ = -4.5f;
			if (player.transform.position.z > 4.5f) camZ = 4.5f;


			//posX = -6; 
			//posZ = -11;

			//garten
			// mitte: 50.944303 - 6.937723
			// oben rechts: 50.945627 - 6.938738
			// unten rechts: 50.943015 - 6.938892

			//evoke kalk
			// mitte 50.935299, 7.008536
			// oben rechts: 50.936348, 7.009322
			// unten rechts: 50.934362, 7.009319

		}

		if (posX < -5 || posX > 5 || posZ < -9 || posZ > 9) inRange = false;
		else inRange = true;

		if (inRange){
			player.transform.localScale = new Vector3(.25f,.25f,.25f);
			player.transform.position = new Vector3(posX,0,posZ);
			playerRotation = new Vector3(0,shipDir,0);
			player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(playerRotation), Time.deltaTime * 5);
			Cam.position = new Vector3(camX,6,camZ-2.5f);
			OutText.SetActive(false);
			
		} else {
			outOfEvoke();
		}

		//out of evoke
		if (posX < -5 || posX > 5 || posZ < -9 || posZ > 9) inRange = false;
		else inRange = true; 

		DebugTextfield.GetComponent<Text>().text = inRange+" "+Input.compass.trueHeading+"\n"+posX+" "+posZ;
	}
	
	private IEnumerator SendPos()
	{
		while(true) 
		{ 
			WWWForm form = new WWWForm();
			form.AddField("name", playername);
			form.AddField("usercode", playercode);
			form.AddField("geopos", posZ+","+posX);
			form.AddField("heading", Input.compass.trueHeading.ToString("R"));
			form.AddField("itemid", 0);
			form.AddField("beaconinrange", 0);
			WWW sendPosition = new WWW(url, form);
			
			yield return sendPosition;

			if (sendPosition.error == null) {
				var N = JSON.Parse(sendPosition.text);
			} else {
//				Debug.Log("Error: "+ sendPosition.error);
			}
//			Debug.Log ("OnCoroutine: "+(int)Time.time); 
			yield return new WaitForSeconds(time);
			}	
	}
	
	void outOfEvoke(){
		OutText.SetActive(true);
		player.transform.localScale = new Vector3(4,4,4);
		player.transform.position = new Vector3(0,5,0);
		player.transform.Rotate(new Vector3(Time.deltaTime * idleSpeed,Time.deltaTime * idleSpeed,0));
		Cam.position = new Vector3(camX,15,camZ-4);
	}

	void Click(string Target){
		if (Target == "PlayerObj") Application.LoadLevel("player");
	}
}
