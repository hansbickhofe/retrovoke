using UnityEngine;
using SimpleJSON;
using System.Collections;
using System.Text;
using System.Reflection;

public class Score : MonoBehaviour {
	
	public GameObject Invader;
	public GameObject Pacman;
	public GameObject Galaga;
	public GameObject BtnBack;
	public string ScoreSpace;
	public string ScorePac;
	public string ScoreGalaga;
	
	//private string url = "https://retrohunter-987.appspot.com/scoresall";
	private string url = "http://localhost:15080/scoresall";
	// Use this for initialization
	void Start () {
		StartCoroutine(GetScore());

	}
	
	void Update () {
		
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
		form.AddField("name", "Allscores");
		
		WWW sendScoreRequest = new WWW(url, form);
		
		yield return sendScoreRequest;
		
		if (sendScoreRequest.error == null) {

			UpdateScores(sendScoreRequest.text);
		} else {
			Debug.Log("Error: "+ sendScoreRequest.error);
		}
	}	

	void UpdateScores (string ScoreRequest)
	{
		var top5 = 0 ;
	
		var TeamShort = "" ;
		var N = JSON.Parse(ScoreRequest);
		ScoreGalaga = N[5]["Team3"].Value.ToString();
		ScorePac = N[5]["Team2"].Value.ToString();
		ScoreSpace = N[5]["Team1"].Value.ToString();

		
		TextMesh teamscoreSpace = GameObject.Find("ScoreSpace").GetComponent<TextMesh>();
		teamscoreSpace.text = ScoreSpace;
		TextMesh teamscorePac = GameObject.Find("ScorePac").GetComponent<TextMesh>();
		teamscorePac.text = ScorePac;
		TextMesh teamscoreGalaga = GameObject.Find("ScoreGalaga").GetComponent<TextMesh>();
		teamscoreGalaga.text = ScoreGalaga;
		
				
		while (top5 <= 5)
		{
			// pedda schau mal hier drüber!
			TextMesh textObject0 = GameObject.Find("name_"+top5.ToString()).GetComponent<TextMesh>();	
			
			int TeamNum = N[top5]["playerdata"][1].AsInt;
			
			if (TeamNum == 1) // 0 = uselected, 1 = invaders, 2 = pac men, 3 = galagas
			{
				TeamShort = "[inv]";
			}
			if (TeamNum == 2) // 0 = uselected, 1 = invaders, 2 = pac men, 3 = galagas
			{
				TeamShort = "[pac]";
			}
			if (TeamNum == 3) // 0 = uselected, 1 = invaders, 2 = pac men, 3 = galagas
			{
				TeamShort = "[gal]";
			}
			
			textObject0.text = (top5+1).ToString()+". " + N[top5]["playername"] + " " + TeamShort + " " + N[top5]["playerdata"][0];
			top5++ ;
		}

	} 	
	void Click(string Target){
		if (Target == "ButtonBack") Application.LoadLevel("player");
	}
	

}