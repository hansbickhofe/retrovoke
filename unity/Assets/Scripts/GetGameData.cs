using UnityEngine;
using SimpleJSON;
using System;
using System.Collections;


public class GetGameData : MonoBehaviour {
	
	public GameObject[] allGoodies;
	//public string[] goodieStat;
	//int goodieID;
	public string PlayerName ;

	private string url = "https://retrohunter-987.appspot.com/pos.getitems";
	// private string url = "http://localhost:15080/pos.getitems";
	public float time = 5;


	// Use this for initialization
	void Start () {
		PlayerName = PlayerPrefs.GetString("playername");
		StartCoroutine(GetGameObjectsTimed());
		//updateGoodies();
	}

// es kommen x datensätz
// pro ds goodietype 0-9
// position im game
// [{"takenby":"HB1","pos":"50.9352136678,7.00834023551","itemid":5418393301680128,"playerid":4855443348258816,"faction":2},

	public void RefreshGameDataOnce(){
		StartCoroutine(GetGameObjectsOnce());
	}

	private IEnumerator GetGameObjectsOnce()
	{	
		WWWForm form = new WWWForm();
		form.AddField("name", PlayerName);
		WWW requestGameObjects = new WWW(url, form);
		
		yield return requestGameObjects;
		
		if (requestGameObjects.error == null) {
			CreateNewGoodie(requestGameObjects.text);
		} else {
			Debug.Log("Error: "+ requestGameObjects.error);
		}
		//	Debug.Log ("OnCoroutine: "+(int)Time.time); 
	}

	private IEnumerator GetGameObjectsTimed()
	{	
		while(true) 
		{ 
			WWWForm form = new WWWForm();
			form.AddField("name", PlayerName);
			WWW requestGameObjects = new WWW(url, form);
			
			yield return requestGameObjects;
			
			if (requestGameObjects.error == null) {
				CreateNewGoodie(requestGameObjects.text);
			} else {
				Debug.Log("Error: "+ requestGameObjects.error);
			}
			//	Debug.Log ("OnCoroutine: "+(int)Time.time); 
			yield return new WaitForSeconds(time);
		}	
	}

	public void CreateNewGoodie(string JSONOutput){
		ResetGoodies(); 
		var N = JSON.Parse(JSONOutput);
		var GoodieCounter = 0 ;

		while (GoodieCounter < N.Count) 
		{
//			Debug.Log("ObjectsJSON: "+JSONOutput);
			string[] PosArray=N[GoodieCounter]["pos"].ToString().Replace("\"", "").Split(',') ;
			string GoodieID = N[GoodieCounter]["itemid"].ToString().Replace("\"", "");
			string TakenBy = N[GoodieCounter]["takenby"].ToString().Replace("\"", "");
			string faction = N[GoodieCounter]["faction"].ToString().Replace("\"", "");
			int ItemType = int.Parse(N[GoodieCounter]["type"]);
			//print (ItemType);

			float z = float.Parse(PosArray[0].Trim());
			float x = float.Parse(PosArray[1].Trim());

			// bitte goodie auf y 0 positionieren, sonst hab ich keine collision
			GameObject newGoodie = Instantiate(allGoodies[ItemType], new Vector3(x,0,z), Quaternion.identity) as GameObject;
			GoodieParams GoodieScript = newGoodie.GetComponent<GoodieParams>();
			GoodieScript.id = GoodieID;
			GoodieScript.takenBy = TakenBy;

			if (TakenBy == "None") {
				GoodieScript.iconText.GetComponent<TextMesh>().text = "00"+ItemType;
			} else if (TakenBy == PlayerName) {
				//goodie neu positionieren
				GoodieScript.posFromPlayer = true;
				GoodieScript.iconText.GetComponent<TextMesh>().text = "YOU";
				print ("you: "+faction);
			} else {
				if (faction == "1") GoodieScript.iconInvader.SetActive(true);
				else if (faction == "2") GoodieScript.iconPacman.SetActive(true);
				else if (faction == "3") GoodieScript.iconGalaga.SetActive(true);
				GoodieScript.iconText.GetComponent<TextMesh>().text = TakenBy;
				print ("enemy: "+faction);
			}

			//in den unterordner schieben
			newGoodie.transform.parent = transform;
			newGoodie.transform.localEulerAngles = new Vector3(90,UnityEngine.Random.Range(0,360),0);
			GoodieCounter++;
		}
	}

	void ResetGoodies(){
		for (int i = 0; i < transform.childCount; i++) {
			//print("i: "+i);
			GameObject.Destroy(transform.GetChild(i).gameObject);
		}
	}

	public void PlayerHitObject(string id){
		print ("Coll Obj ID: "+id);
	}
}
