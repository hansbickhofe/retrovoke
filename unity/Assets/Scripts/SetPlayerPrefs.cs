using UnityEngine;
using System.Collections;

public class SetPlayerPrefs : MonoBehaviour {
	public string playername ;
	public string playercode ;
	public int playerteam ;
	// Use this for initialization
	// HOB == MXAJ147321ZW 
	// HHH == HXSJ947537HT
	// PPP == WCXU877693KA
	// PET == MXWH558618KY
	// PED == NNYG696505WZ	
	void Start () {
		PlayerPrefs.SetString("playername","HOB");
		PlayerPrefs.SetString("playercode","MXAJ147321ZW");
		PlayerPrefs.SetInt("playerteam",2);	
		playername = PlayerPrefs.GetString("playername");
		playercode = PlayerPrefs.GetString("playercode");
		playerteam = PlayerPrefs.GetInt("playerteam");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
