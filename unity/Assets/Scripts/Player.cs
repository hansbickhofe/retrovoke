using UnityEngine;
using SimpleJSON;
using System.Collections;

public class Player : MonoBehaviour {
	
	public GameObject Invader;
	public GameObject Pacman;
	public GameObject Galaga;
	public GameObject BtnScore;
	public GameObject BtnBack;
	public string playername;
	public int playerteam; // 0 = uselected, 1 = invaders, 2 = pac men, 3 = galagas
	public string playertotalscore;
 	//private string url = "https://retrohunter-987.appspot.com/score";
	private string url = "http://localhost:15080/score";
	
	// Use this for initialization
	void Start () {
		playername = PlayerPrefs.GetString("playername");
		playerteam = PlayerPrefs.GetInt("playerteam");
		StartCoroutine(GetScore());
		TextMesh textObject0 = GameObject.Find("Payerscore").GetComponent<TextMesh>();
		textObject0.text = playertotalscore;

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
	}
	
	void Update () {
	
		TextMesh textObject0 = GameObject.Find("Payerscore").GetComponent<TextMesh>();
		textObject0.text = playertotalscore;
		TextMesh textObject1 = GameObject.Find("Payername").GetComponent<TextMesh>();
		textObject1.text = playername;
		
		
		//mouse
		if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitClick;
			
			if (Physics.Raycast(ray, out hitClick)){
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
				Debug.Log ("Touched: "+hit.collider.name);
				Click (hit.collider.name);
			}
		}
	}
	
	private IEnumerator GetScore()
	{
			
		WWWForm form = new WWWForm();
		form.AddField("name", playername);

		WWW sendScoreRequest = new WWW(url, form);
		
		yield return sendScoreRequest;
		
		if (sendScoreRequest.error == null) {
			var N = JSON.Parse(sendScoreRequest.text);
			playertotalscore = N["TotalScore"];
			Debug.Log(N);
		} else {
			Debug.Log("Error: "+ sendScoreRequest.error);
		}
	}	

	
	void Click(string Target){
		if (Target == "Teams") Application.LoadLevel("game");
		if (Target == "ButtonScore") Application.LoadLevel("score");
		if (Target == "ButtonAbout") Application.LoadLevel("about");
	}
}