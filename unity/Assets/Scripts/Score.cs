using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	
	public GameObject Invader;
	public GameObject Pacman;
	public GameObject Galaga;
	public GameObject BtnBack;
	
	// Use this for initialization
	void Start () {

		TextMesh teamscoreSpace = GameObject.Find("ScoreSpace").GetComponent<TextMesh>();
		teamscoreSpace.text = "111";
		TextMesh teamscorePac = GameObject.Find("ScorePac").GetComponent<TextMesh>();
		teamscorePac.text = "222";
		TextMesh teamscoreGalaga = GameObject.Find("ScoreGalaga").GetComponent<TextMesh>();
		teamscoreGalaga.text = "333";


		TextMesh textObject0 = GameObject.Find("name_0").GetComponent<TextMesh>();
		textObject0.text = "1.abc [gal] 1234";
		TextMesh textObject1 = GameObject.Find("name_1").GetComponent<TextMesh>();
		textObject1.text = "2.def [gal] 1234";
		TextMesh textObject2 = GameObject.Find("name_2").GetComponent<TextMesh>();
		textObject2.text = "3.ghi [inv] 1234";
		TextMesh textObject3 = GameObject.Find("name_3").GetComponent<TextMesh>();
		textObject3.text = "4.ghi [pac] 1234";
		TextMesh textObject4 = GameObject.Find("name_4").GetComponent<TextMesh>();
		textObject4.text = "5.ghi [gal] 1234";
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
	
	void Click(string Target){
		if (Target == "ButtonBack") Application.LoadLevel("player");
	}
}