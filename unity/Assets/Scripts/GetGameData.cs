using UnityEngine;
using System.Collections;

public class GetGameData : MonoBehaviour {


	public GameObject[] allGoodies;
	public string[] goodieStat;
	int goodieID;


	// Use this for initialization
	void Start () {
		updateGoodies();
	}

	void updateGoodies(){
		for (int i=0; i<goodieStat.Length; i++){
			if (goodieStat[i] == "available"){
				GameObject newGoodie = Instantiate(allGoodies[selectGoodieType()], transform.position, Quaternion.identity) as GameObject;
				newGoodie.transform.parent = transform;
				newGoodie.transform.localEulerAngles = new Vector3(90,0,0);

				//spawnzone auswählen
				if (Random.value < 0.5f) {
					// zone 0
					newGoodie.transform.position = new Vector3(Random.Range(-2.5f,0.5f),.1f,Random.Range(0,5.5f));
				} else {
					// zone 1
					newGoodie.transform.position = new Vector3(Random.Range(2.5f,4.5f),.1f,Random.Range(6.5f,-7.5f));
				}

				//set goodie params
				goodieStat[i] = "active";
				print(Random.value);
			}
		}
	}

	int selectGoodieType(){
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
		return goodieID;
	}
}
