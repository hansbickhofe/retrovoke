using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class LoginMenu : MonoBehaviour {

	public string curPage;
	string[] pages = new string[]{"entername","checkname","welcome","selectteam","entercode","playerstat","about","scoreboard"}; 

	public GameObject Logo;
	public GameObject Subline;
	public GameObject InputField;
	public GameObject Teams;
	public GameObject InfoTxt;
	public GameObject BtnAction;
	public GameObject ActionTxt;
	public GameObject BtnAbout;
	public GameObject BtnBack;
	public GameObject WarnTxt;
	public string myTeam = "Unselected";
	
	// hans codezeuchs
	//private string url = "http://localhost:15080/config.requestcode";
	private string url = "https://retrohunter-987.appspot.com/config.requestcode";
	string playername;
	string playercode;
	int playerteam; // 0 = uselected, 1 = invaders, 2 = pac men, 3 = galagas, 

	//debug stuff
	bool nameExist;
	bool loginError;
	//debug stuff ende


	// Use this for initialization
	void Start () {

		//debug stuff
		nameExist = false;
		loginError = false;
		//debug stuff ende

		curPage = "entername";
		ShowPage();
		playername = PlayerPrefs.GetString("playername");
	}

	public void ShowPage(){
		if (curPage == "entername"){
			Logo.SetActive(true);
			InputField.SetActive(true);
			Teams.SetActive(false);
			
			InfoTxt.SetActive(true);
			InfoTxt.GetComponent<TextMesh>().text = "ENTER YOUR \nTHREE DIGIT NAME";
			
			BtnAction.SetActive(true);
			ActionTxt.GetComponent<TextMesh>().text = "SUBMIT";
		
			BtnAbout.SetActive(true);
			BtnBack.SetActive(false);
			WarnTxt.SetActive(false);

			//reset bei back
			myTeam = "unselected";  

		}

		if (curPage == "reentername"){
			Logo.SetActive(true);
			InputField.SetActive(true);
			Teams.SetActive(false);
			
			InfoTxt.SetActive(true);
			InfoTxt.GetComponent<TextMesh>().text = "NAME ALREADY IN USE.\nPLEASE ENTER ANOTHER\nTHREE DIGIT NAME.";
			
			BtnAction.SetActive(true);
			ActionTxt.GetComponent<TextMesh>().text = "SUBMIT";
			
			BtnAbout.SetActive(true);
			BtnBack.SetActive(false);
			WarnTxt.SetActive(false);
			
			//reset bei back
			myTeam = "unselected";  
		}

		if (curPage == "loginError"){
			Logo.SetActive(true);
			InputField.SetActive(true);
			Teams.SetActive(false);
			
			InfoTxt.SetActive(true);
			InfoTxt.GetComponent<TextMesh>().text = "ERROR!\nMEANWHILE ANOTHER PLAYER\nused your name.\nPLEASE ENTER a new\nTHREE DIGIT NAME.";
			
			BtnAction.SetActive(true);
			ActionTxt.GetComponent<TextMesh>().text = "SUBMIT";
			
			BtnAbout.SetActive(true);
			BtnBack.SetActive(false);
			WarnTxt.SetActive(false);
			
			//reset bei back
			myTeam = "unselected";  
		}

		else if (curPage == "checkname"){
			Logo.SetActive(true);
			InputField.SetActive(true);
			Teams.SetActive(false);
			
			InfoTxt.SetActive(true);
			InfoTxt.GetComponent<TextMesh>().text = "CHECKING...";
			
			BtnAction.SetActive(false);
			BtnAbout.SetActive(false);
			BtnBack.SetActive(false);
			WarnTxt.SetActive(false);

			// coroutine zum namenscheck aufrufen (name vorhanden oder frei)
			// welcom cooroutine feststellt das der name frei ist 
			// dann zweite coroutine starten und code anfordern

//			WWWForm form = new WWWForm();
//			form.AddField("name", playername);
//			WWW www = new WWW(url, form);
//			StartCoroutine(WaitForCode(www));
			
			if (nameExist == false){
				playername = PlayerPrefs.GetString("playername");
				WWWForm form = new WWWForm();
				form.AddField("name", playername);
				WWW www = new WWW(url, form);
				StartCoroutine(WaitForCode(www));
			} else {
				curPage = "reentername";
				ShowPage();
			}
		}

		else if (curPage == "welcome"){
			Logo.SetActive(true);
			InputField.SetActive(true);
			Teams.SetActive(false);
			
			InfoTxt.SetActive(true);
			InfoTxt.GetComponent<TextMesh>().text = "Welcome, "+playername+"!\nYour private secret is\n"+playercode;
			
			BtnAction.SetActive(true);
			ActionTxt.GetComponent<TextMesh>().text = "CONTINUE";

			BtnAbout.SetActive(false);
			BtnBack.SetActive(false);

			WarnTxt.SetActive(true);
			WarnTxt.GetComponent<TextMesh>().text = "WARNING!\nDo not loose \nyour secret code!";

		}

		else if (curPage == "selectteam"){
			Logo.SetActive(true);
			InputField.SetActive(false);
			Teams.SetActive(true);
			
			InfoTxt.SetActive(true);
			InfoTxt.GetComponent<TextMesh>().text = "PLEASE,\nSELECT YOUR TEAM\n[ "+myTeam+" ] ";
			
			if (myTeam != "unselected") BtnAction.SetActive(true);
			else BtnAction.SetActive(false);

			ActionTxt.GetComponent<TextMesh>().text = "SELECT";
			
			BtnAbout.SetActive(false);
			BtnBack.SetActive(false);

			WarnTxt.SetActive(true);
			WarnTxt.GetComponent<TextMesh>().text = "WARNING!\nYou can’t change \nyour team later!";
		}

		else if (curPage == "configcheck"){
			Logo.SetActive(true);
			InputField.SetActive(true);
			Teams.SetActive(false);
			
			InfoTxt.SetActive(true);
			InfoTxt.GetComponent<TextMesh>().text = "LOGIN IN PROGRESS.\nPLEASE WAIT...";
			
			BtnAction.SetActive(false);
			BtnAbout.SetActive(false);
			BtnBack.SetActive(false);
			WarnTxt.SetActive(false);

			// coroutine mit allen drei varaiablen senden und prüfen ob nicht irgend ein kackarsch dazwischengrrgräscht ist
			// falls ja, spruch zum anfang 
			if (loginError) {
				curPage = "loginError";
				ShowPage();
			} else {

				if (myTeam == "invaders") playerteam = 1;
				else if (myTeam == "pac_men") playerteam = 2;
				else if (myTeam == "galagas") playerteam = 3;

				PlayerPrefs.SetString("playercode", playercode);
				PlayerPrefs.SetInt("playerteam", playerteam);
				Application.LoadLevel("game");
			}

		}

		else if (curPage == "entercode"){
			Logo.SetActive(true);
			InputField.SetActive(true);
			Teams.SetActive(false);
			
			InfoTxt.SetActive(true);
			InfoTxt.GetComponent<TextMesh>().text = playername+" is already in \nuse. Please enter \nyour secret code.";
			
			BtnAction.SetActive(true);
			ActionTxt.GetComponent<TextMesh>().text = "LOGIN";
			
			BtnAbout.SetActive(false);
			BtnBack.SetActive(true);
			
			WarnTxt.SetActive(false);
		}
	}

	public void CheckName(string username){
		PlayerPrefs.SetString("playername", username);
		Debug.Log (PlayerPrefs.GetString("playername"));
	}

	IEnumerator Wait(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		curPage = "welcome";
		ShowPage();
	}

	IEnumerator WaitForCode(WWW nextFreePlayerCode)
	{
		yield return nextFreePlayerCode;
		
		// check for errors
		if (nextFreePlayerCode.error == null) {
			var N = JSON.Parse(nextFreePlayerCode.text);
			PlayerPrefs.SetString("code",N["playercode"]);
			playercode = PlayerPrefs.GetString("code");
			Debug.Log ("Prefs: "+playercode);
			curPage = "welcome";
			ShowPage();
		} else {
			Debug.Log("Error: "+ nextFreePlayerCode.error);
		} 
	}
}