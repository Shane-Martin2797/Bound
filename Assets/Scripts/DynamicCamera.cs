using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicCamera : MonoBehaviour
{
	public List<GameObject> gameObjectsToFollow = new List<GameObject> ();
	
	// Update is called once per frame
	void Update ()
	{
		if (gameObjectsToFollow.Count > 0) {
			FollowGameObjects ();
			AdjustCameraSize ();
		}
	}
	
	void FollowGameObjects ()
	{
		Vector3 positionAverage = Vector3.zero;
		for (int i = 0; i < gameObjectsToFollow.Count; ++i) {
			positionAverage += gameObjectsToFollow [i].transform.position;
		}
		positionAverage /= gameObjectsToFollow.Count;
		positionAverage.z = transform.position.z;
		transform.position = positionAverage;
		
	}
	void AdjustCameraSize ()
	{
		float currentMaxDistance = 0;
		for (int i= 0; i < gameObjectsToFollow.Count; ++i) {
			for (int j = 0; j < gameObjectsToFollow.Count; ++j) {
				if (j != i) {
					float distance = Vector3.Distance (gameObjectsToFollow [i].transform.position, gameObjectsToFollow [j].transform.position);
					if (distance > currentMaxDistance) {
						currentMaxDistance = distance;
					}
				}
			}
		}
		Vector3 pos = transform.position;
		pos.z = -currentMaxDistance * 1.35f;
		transform.position = pos;
	}
}
