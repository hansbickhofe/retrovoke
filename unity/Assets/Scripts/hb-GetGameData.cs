using UnityEngine;
using SimpleJSON;
using System;
using System.Collections;

public class GetGameData : MonoBehaviour {

	public GameObject[] allGoodies;
<<<<<<< HEAD
	private string url = "https://retrohunter-987.appspot.com/pos.getitems";
	// private string url = "http://localhost:15080/pos.getitems";
	public float time = 5;

	// Use this for initialization
	void Start () {
		StartCoroutine(GetGameObjects());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
			//			Debug.Log ("OnCoroutine: "+(int)Time.time); 
			yield return new WaitForSeconds(time);
		}	
	}

	public void CreateNewGoodie(string JSONOutput){
		var N = JSON.Parse(JSONOutput);
		var GoddieCounter = 0 ;
		while (GoddieCounter < N.Count) 
		{
			Debug.Log("ObjectsJSON: "+GoddieCounter.ToString() + ": "  + N[GoddieCounter]["itemid"]+ " " + N[GoddieCounter]["pos"]);
			string[] PosArray=N[GoddieCounter]["pos"].ToString().Replace("\"", "").Split(',') ;
			string GoodieID = N[GoddieCounter]["itemid"].ToString().Replace("\"", "");
			float z = float.Parse(PosArray[0].Trim());
			float x = float.Parse(PosArray[1].Trim());
			allGoodies[GoddieCounter].transform.position = new Vector3(x, -1 ,z );
			allGoodies[GoddieCounter].SetActive(true);
			
			GoddieCounter++;
		}
		

		//Instantiate(newGoodie, new Vector3(xPos, 0, zPos), Quaternion.identity);
=======
	public int maxGoodies = 5;
	int goodieID;


	// Use this for initialization
	void Start () {
		chooseGoodies();
	}

	void chooseGoodies(){
		for (int i=0; i<maxGoodies; i++){
			int rnd = Random.Range(0,100);
			
			if (rnd == 100) goodieID = 9;
			else if (rnd >= 98 && rnd < 100) goodieID = 8;
			else if (rnd >= 95 && rnd < 98)  goodieID = 7;
			else if (rnd >= 90 && rnd < 95)  goodieID = 6;
			else if (rnd >= 80 && rnd < 90)  goodieID = 5;
			else if (rnd >= 70 && rnd < 80)  goodieID = 4;
			else if (rnd >= 50 && rnd < 60)  goodieID = 3;
			else if (rnd >= 40 && rnd < 50)  goodieID = 2;
			else if (rnd >= 20 && rnd < 40)  goodieID = 1;
			else if (rnd >= 0 && rnd < 20)  goodieID = 0;

			print(goodieID);
		}


>>>>>>> ab6250bc076fc1a6fb9b29879d7ec035b0c9f3a6
	}
}
