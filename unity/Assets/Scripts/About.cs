using UnityEngine;
using System.Collections;

public class About : MonoBehaviour {

	public GameObject Invader;
	public GameObject Pacman;
	public GameObject Galaga;
	public GameObject BtnBack;
	
	// Use this for initialization
	void Start () {
	
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
		if (Target == "ButtonBack" && PlayerPrefs.GetString("LoginComplete") == "ACK") Application.LoadLevel("game");
		else Application.LoadLevel("login");

//		if (Target == "space") Application.LoadLevel("player");
//		if (Target == "pac") Application.LoadLevel("score");
//		if (Target == "galaga") Application.LoadLevel("login");
	}
}

