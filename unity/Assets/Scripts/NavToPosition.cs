using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections;

public class NavToPosition : MonoBehaviour {

	GetLocation myGPS;
	public GameObject DebugTextfield;

	public float lat = 50.944303f;
	public float lon =  6.937723f;
	public float multiX = 4500;
	public float multiY = 7000;
	public float time = 3;
	private string url = "https://focus-sweep-87123.appspot.com/";
	// private string url = "http://localhost:15080/pos";

	float posX;
	float posZ;

	float shipDir;

	public GameObject player;
	Vector3 playerRotation;

	public int idleSpeed;
	bool inRange = false;
	public GameObject OutText;

	public float demoSpeed;
	public string playername;
	public string playercode;
	public int playerteam;

	// Use this for initialization
	void Start () {
		myGPS = GetComponent<GetLocation>();
		
		posX = (6.937723f - lon)*multiX;
		posZ = (50.944303f - lat)*multiY;
		
		playername = PlayerPrefs.GetString("playername");
		playercode = PlayerPrefs.GetString("playercode");
		playerteam = PlayerPrefs.GetInt("playerteam");
		StartCoroutine(SendPos());
		
		
	}
	
	// Update is called once per frame
	void Update () {
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

			//posX = -6; 
			//posZ = -11;

			// mitte: 50.944303 - 6.937723
			// oben rechts: 50.945627 - 6.938738
			// unten rechts: 50.943015 - 6.938892
		}

		if (inRange){
			player.transform.localScale = new Vector3(.5f,.5f,.5f);
			player.transform.position = new Vector3(posX,0,posZ);
			playerRotation = new Vector3(0,shipDir,0);
			player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(playerRotation), Time.deltaTime * 5);
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
			form.AddField("code", playercode);
			form.AddField("pos", posZ+","+posX);
			form.AddField("heading", Input.compass.trueHeading.ToString("R"));
			form.AddField("itemid", 0);
			form.AddField("beaconinrange", 0);
			WWW sendPosition = new WWW(url, form);
			
			yield return sendPosition;

			if (sendPosition.error == null) {
				var N = JSON.Parse(sendPosition.text);
			} else {
				Debug.Log("Error: "+ sendPosition.error);
			}
			Debug.Log ("OnCoroutine: "+(int)Time.time); 
			yield return new WaitForSeconds(time);
			}
			
			
			
	}
	
		
	
	
	void outOfEvoke(){
		OutText.SetActive(true);
		player.transform.localScale = new Vector3(4,4,4);
		player.transform.position = new Vector3(0,5,0);
		player.transform.Rotate(new Vector3(Time.deltaTime * idleSpeed,Time.deltaTime * idleSpeed,0));
	}
}
