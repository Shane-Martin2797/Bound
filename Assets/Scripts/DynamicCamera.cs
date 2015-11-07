using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicCamera : MonoBehaviour
{
	public List<GameObject> gameObjectsToFollow = new List<GameObject> ();
	public float minDistance = 12;
	private float modifier = 1.25f;
	
	void Awake ()
	{
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameObjectsToFollow.Count > 0) {
			FollowGameObjects ();
		}
	}
	
	void FollowGameObjects ()
	{
		//Vector3 pos1ForAverage = Vector3.zero;
		//Vector3 pos2ForAverage = Vector3.zero;
		
		Vector3 positionAverage = Vector3.zero;
		
		float currentMaxDistance = 0;
		for (int i= 0; i < gameObjectsToFollow.Count; ++i) {
			for (int j = 0; j < gameObjectsToFollow.Count; ++j) {
				if (j != i) {
					float distance = Vector3.Distance (gameObjectsToFollow [i].transform.position, gameObjectsToFollow [j].transform.position);
					if (distance >= currentMaxDistance) {
						currentMaxDistance = distance;
						//pos1ForAverage = gameObjectsToFollow [i].transform.position;
						//pos2ForAverage = gameObjectsToFollow [j].transform.position;
					}
				}
			}
		}
		
		for (int i = 0; i < gameObjectsToFollow.Count; ++i) {
			positionAverage += (gameObjectsToFollow [i].transform.position);
		}
		
		positionAverage /= gameObjectsToFollow.Count;
		
		//Vector3 positionAverage = (pos1ForAverage + pos2ForAverage) / 2;
		transform.position = positionAverage;
		AdjustCameraSize (currentMaxDistance);
		
	}
	void AdjustCameraSize (float dist)
	{
		dist *= modifier;
		if (dist < minDistance) {
			dist = minDistance;
		} 
		Vector3 pos = transform.position + (-transform.forward * (dist));
		
		transform.position = pos;
	}
}
