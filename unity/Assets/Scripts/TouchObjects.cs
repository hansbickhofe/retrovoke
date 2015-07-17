using UnityEngine;
using System.Collections;

public class TouchObjects : MonoBehaviour {

	LoginMenu mainScript;
	string stringToEdit = "Debug";

	void Start(){
		mainScript = GetComponent<LoginMenu>();
	}

	void Update () {

		//mouse
		if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitClick;

			if (Physics.Raycast(ray, out hitClick)){
				stringToEdit = "Touched: "+hitClick.collider.name;
				Debug.Log ("Touched: "+hitClick.collider.name);
				Click (hitClick.collider.name);
			}
		}
			
			// touch
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch (0).position)), Vector2.zero);

			if (hit.collider != null)
			{
				stringToEdit = "Touched: "+hit.collider.name;
				Debug.Log ("Touched: "+hit.collider.name);
				Click (hit.collider.name);
			}
		}
	}

	void Click(string Target){
		if (mainScript.curPage == "entername" || mainScript.curPage == "reentername" || mainScript.curPage == "loginError"){
			if (Target == "ButtonAction") mainScript.curPage = "checkname";
			if (Target == "ButtonAbout") Application.LoadLevel("about");
		}

		else if (mainScript.curPage == "welcome"){
			if (Target == "ButtonAction") mainScript.curPage = "selectteam";
		}

		else if (mainScript.curPage == "selectteam"){
			if (Target == "space") mainScript.myTeam = "invaders";
			if (Target == "pac") mainScript.myTeam = "pac_men";
			if (Target == "galaga") mainScript.myTeam = "galagas";

			if (Target == "ButtonAction") mainScript.curPage = "configcheck";
		}

		else if (mainScript.curPage == "entercode"){
			if (Target == "ButtonAction") mainScript.curPage = "entercode";
			if (Target == "ButtonBack") mainScript.curPage = "entername";
		}

		mainScript.ShowPage();
	}

	void OnGUI() {
		stringToEdit = GUI.TextField(new Rect(0, 0, 400, 50), stringToEdit, 25);
	}
}
