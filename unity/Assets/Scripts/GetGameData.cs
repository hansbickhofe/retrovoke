using UnityEngine;
using System.Collections;

public class GetGameData : MonoBehaviour {

	public GameObject[] allGoodies;
	public int maxGoodies = 5;
	int goodieID;


	// Use this for initialization
	void Start () {
		chooseGoodies();
	}

	void chooseGoodies(){
		for (int i=0; i<maxGoodies; i++){
			int rnd = Random.Range(0,100);
			
			if (rnd == 100) goodieID = 9;
			else if (rnd >= 98 && rnd < 100) goodieID = 8;
			else if (rnd >= 95 && rnd < 98)  goodieID = 7;
			else if (rnd >= 90 && rnd < 95)  goodieID = 6;
			else if (rnd >= 80 && rnd < 90)  goodieID = 5;
			else if (rnd >= 70 && rnd < 80)  goodieID = 4;
			else if (rnd >= 50 && rnd < 60)  goodieID = 3;
			else if (rnd >= 40 && rnd < 50)  goodieID = 2;
			else if (rnd >= 20 && rnd < 40)  goodieID = 1;
			else if (rnd >= 0 && rnd < 20)  goodieID = 0;

			print(goodieID);
		}


	}
}
