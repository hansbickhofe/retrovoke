using UnityEngine;
using System.Collections;

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

	public string myTeam = "unselected";  
	
	// Use this for initialization
	void Start () {
		curPage = "entername";
		ShowPage();
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

			StartCoroutine(Wait(3.0F));


		}

		else if (curPage == "welcome"){
			Logo.SetActive(true);
			InputField.SetActive(true);
			Teams.SetActive(false);
			
			InfoTxt.SetActive(true);
			InfoTxt.GetComponent<TextMesh>().text = "Welcome, ABC!\nYour private secret is\nJ675J";
			
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

		else if (curPage == "entercode"){
			Logo.SetActive(true);
			InputField.SetActive(true);
			Teams.SetActive(false);
			
			InfoTxt.SetActive(true);
			InfoTxt.GetComponent<TextMesh>().text = "ABC is already in \nuse. Please enter \nyour secret code.";
			
			BtnAction.SetActive(true);
			ActionTxt.GetComponent<TextMesh>().text = "LOGIN";
			
			BtnAbout.SetActive(false);
			BtnBack.SetActive(true);
			
			WarnTxt.SetActive(false);
		}
	}

	IEnumerator Wait(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		curPage = "welcome";
		ShowPage();
	}
}