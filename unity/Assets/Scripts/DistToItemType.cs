using UnityEngine;
using System.Collections;

public class DistToItemType : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		distToItemType();
	}

	void distToItemType(){

		float rndValueX = Random.Range (-10f, 10f);
		float rndValueY = Random.Range (-10f, 10f);

		int itemType = 0;
		//inner circle
		if (rndValueX > -5 && rndValueX < 3 && rndValueY > -3 && rndValueY < 3) {
			itemType = Random.Range (0,4);
		} else if (rndValueX > -7.5f && rndValueX < 7.5f && rndValueY > -7.5f && rndValueY < 7.5f) {
			itemType = Random.Range (5,7);
		} else if (rndValueX >= -10f && rndValueX <= 10f && rndValueY >= -10 && rndValueY <= 10) {
			itemType = Random.Range (8,10);
		}

		Debug.Log (itemType);
	}
}
