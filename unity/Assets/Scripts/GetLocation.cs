﻿using UnityEngine;
using System.Collections;

public class GetLocation : MonoBehaviour
{
	string output = "Debug";

	void Start() {
		output = "Start";
		StartCoroutine("StartGPS");
	}


	IEnumerator StartGPS()
	{
		output = "Searching...";
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			yield break;
		
		// Start service before querying location
		Input.location.Start();
		
		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		
		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			//print("Timed out");
			output = "Timed out";
			yield break;
		}
		
		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			//print("Unable to determine device location");
			output = "Unable to determine device location";
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			//swprint("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			output = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
		}
		
		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
	}

	void OnGUI() {
		output = GUI.TextField(new Rect(0, 0, 400, 50), output, 25);
	}
}