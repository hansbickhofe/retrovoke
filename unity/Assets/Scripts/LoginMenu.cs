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
		private string coderequesturl = "https://retrohunter-987.appspot.com/config.requestcode";
		private string namecheckurl = "https://retrohunter-987.appspot.com/config.checkname";
		private string configurl = "https://retrohunter-987.appspot.com/config.configplayer";	
	//	public string coderequesturl = "http://localhost:15080/config.requestcode";
	//	public string namecheckurl = "http://localhost:15080/config.checkname";
	//	public string configurl = "http://localhost:15080/config.configplayer";
	public string playername;
	public string playercode;
	int playerteam; // 0 = uselected, 1 = invaders, 2 = pac men, 3 = galagas, 
	int checkNumber ;

	public bool nameExist;
	public bool loginError;


	// Use this for initialization
	void Start () {

		nameExist = false;
		loginError = false;

		curPage = "entername";
		ShowPage();
		//PlayerPrefs.DeleteAll();
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
			checkNumber = 2;
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
			
			checkNumber = 1;
			NameCheckRequest(PlayerPrefs.GetString("playername"),checkNumber);
			
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
			checkNumber = 2;
			Logo.SetActive(true);
			InputField.SetActive(true);
			Teams.SetActive(false);
			
			InfoTxt.SetActive(true);
			InfoTxt.GetComponent<TextMesh>().text = "LOGIN IN PROGRESS.\nPLEASE WAIT...";
			
			BtnAction.SetActive(false);
			BtnAbout.SetActive(false);
			BtnBack.SetActive(false);
			WarnTxt.SetActive(false);
			

			
			if (loginError) {
				curPage = "loginError";
				ShowPage();
			} else {

				if (myTeam == "invaders") playerteam = 1;
				else if (myTeam == "pac_men") playerteam = 2;
				else if (myTeam == "galagas") playerteam = 3;

				PlayerPrefs.SetString("playercode", playercode);
				PlayerPrefs.SetInt("playerteam", playerteam);
				NameCheckRequest(PlayerPrefs.GetString("playername"),checkNumber);
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
//		Debug.Log ("Name entered: "+PlayerPrefs.GetString("playername"));
	}
	
	
	public void NameCheckRequest(string playername, int checkNumber) {
		WWWForm nameform = new WWWForm();
		nameform.AddField("name", playername);
		WWW www1 = new WWW(namecheckurl, nameform);
		StartCoroutine(WaitForNameCheck(www1,checkNumber,playername));
	}
	
	public void ConfigRequest() {
		playername = PlayerPrefs.GetString("playername");
		playercode = PlayerPrefs.GetString("playercode");
		playerteam = PlayerPrefs.GetInt("playerteam");
		
		WWWForm configform = new WWWForm();
		configform.AddField("name", playername);
		configform.AddField("code", playercode);
		configform.AddField("faction", playerteam);
		WWW wwwconfig = new WWW(configurl, configform);
		StartCoroutine(WaitForConfigCheck(wwwconfig));
		
	}

	IEnumerator WaitForConfigCheck(WWW playerConfig)
	{
//		Debug.Log ("ConfigCheckRequest called");
		yield return playerConfig;
		
		// check for errors
		if (playerConfig.error == null) {
			var N = JSON.Parse(playerConfig.text);
			string response = N["response"];
			
			if (response == "NAMENACK" || response == "CODENACK" ) {
//				Debug.Log ("Configcheck false: "+response);
				curPage = "loginError";
				ShowPage();
			}
			else if (response == "ACK"){
				nameExist = false;
//				Debug.Log ("Configcheck true: "+ response);
				PlayerPrefs.SetString("LoginComplete","ACK");
				Application.LoadLevel("game");
			}
		} 
		else {
			Debug.Log("Error: "+ playerConfig.error);
			curPage = "loginError";
			ShowPage();
		}
		
	}

	IEnumerator WaitForNameCheck(WWW playerNameFree, int checkNumber,string playername)
	{
//		Debug.Log ("NameCheckRequest called");
		yield return playerNameFree;
		
		// check for errors
		if (playerNameFree.error == null) {
			var N = JSON.Parse(playerNameFree.text);
			string response = N["response"];
			
			if (response == "NAMENACK") {
				nameExist = true;
				PlayerPrefs.SetString("playername", "");
//				Debug.Log ("NameCheck false: "+response);
				curPage = "reentername";
				ShowPage();
			}
			else if (response == "ACK"){
				nameExist = false;
				PlayerPrefs.SetString("playername", playername);
//				Debug.Log ("NameCheck true: "+ response);
								
				WWWForm codeform = new WWWForm();
				codeform.AddField("name", playername);
				WWW www = new WWW(coderequesturl, codeform);
				if (checkNumber == 1 ) StartCoroutine(WaitForCode(www));
				if (checkNumber == 2 ) ConfigRequest();

			}
		} 
		else {
//			Debug.Log("Error: "+ playerNameFree.error);
			curPage = "loginError";
			ShowPage();
		}
		
	}
	
	IEnumerator WaitForCode(WWW nextFreePlayerCode)
	{
		yield return nextFreePlayerCode;
		
		// check for errors
		if (nextFreePlayerCode.error == null) {
			var N = JSON.Parse(nextFreePlayerCode.text);
			PlayerPrefs.SetString("playercode",N["playercode"]);
			PlayerPrefs.SetString("playername",N["playername"]);
			playercode = PlayerPrefs.GetString("playercode");
			playername = PlayerPrefs.GetString("playername");
//			Debug.Log ("playercode: " + playercode);
//			Debug.Log ("playername: " + playername);
			curPage = "welcome";
			ShowPage();
		} else {
//			Debug.Log("Error: "+ nextFreePlayerCode.error);
			curPage = "loginError";
			ShowPage();			
		}
	}	
}