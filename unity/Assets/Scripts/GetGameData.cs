using UnityEngine;
using SimpleJSON;
using System;
using System.Collections;


public class GetGameData : MonoBehaviour {

	public GameObject[] allGoodies;
	//public string[] goodieStat;
	//int goodieID;

	private string url = "https://retrohunter-987.appspot.com/pos.getitems";
	public float time = 5;


	// Use this for initialization
	void Start () {
		StartCoroutine(GetGameObjects());
		//updateGoodies();
	}

// es kommen x datensätz
// pro ds goodietype 0-9
// position im game
// [{"takenby":"HB1","pos":"50.9352136678,7.00834023551","itemid":5418393301680128,"playerid":4855443348258816,"faction":2},

	private IEnumerator GetGameObjects()
	{	
		while(true) 
		{ 
			WWWForm form = new WWWForm();
			form.AddField("name", "Allplayers");
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
			//Debug.Log("ObjectsJSON: "+GoodieCounter.ToString() + ": "  + N[GoodieCounter]["itemid"]+ " " + N[GoodieCounter]["pos"]);
			string[] PosArray=N[GoodieCounter]["pos"].ToString().Replace("\"", "").Split(',') ;
			string GoodieID = N[GoodieCounter]["itemid"].ToString().Replace("\"", "");
			string TakenBy = N[GoodieCounter]["takenby"].ToString().Replace("\"", "");

			float z = float.Parse(PosArray[0].Trim());
			float x = float.Parse(PosArray[1].Trim());

			// bitte goodie auf y 0 positionieren, sonst hab ich keine collision
			GameObject newGoodie = Instantiate(allGoodies[5], new Vector3(x,0,z ), Quaternion.identity) as GameObject;
			newGoodie.GetComponent<GoodieParams>().id = GoodieID;
			newGoodie.GetComponent<GoodieParams>().takenBy = TakenBy;
			newGoodie.transform.parent = transform;
			newGoodie.transform.localEulerAngles = new Vector3(90,0,0);

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
