using UnityEngine;
using SimpleJSON;
using System;
using System.Collections;

public class GetGameData : MonoBehaviour {

	public GameObject[] allGoodies;
	private string url = "https://retrohunter-987.appspot.com/pos.getitems";
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
	}
}
