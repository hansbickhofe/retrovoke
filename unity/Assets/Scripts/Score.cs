using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	
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
		if (Target == "ButtonBack") Application.LoadLevel("login");
	}
}