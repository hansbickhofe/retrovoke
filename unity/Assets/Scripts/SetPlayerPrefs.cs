using UnityEngine;
using System.Collections;

public class SetPlayerPrefs : MonoBehaviour {
	public string playername ;
	public string playercode ;
	public int playerteam ;
	// Use this for initialization
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
