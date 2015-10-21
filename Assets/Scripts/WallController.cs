using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour
{	
	void OnCollisionEnter (Collision col)
	{
		NotifyEnter (col.gameObject);
	}
	void OnCollisionExit (Collision col)
	{
		NotifyExit (col.gameObject);
	}

	void OnTriggerEnter (Collider col)
	{
		NotifyEnter (col.gameObject);
	}
	void OnTriggerExit (Collider col)
	{
		NotifyExit (col.gameObject);
	}
	
	void NotifyEnter (GameObject gameObj)
	{
		PlayerController player = gameObj.GetComponent<PlayerController> ();
		if (player != null) {
			player.targetWall = this;
		}
	}
	void NotifyExit (GameObject gameObj)
	{
		PlayerController player = gameObj.GetComponent<PlayerController> ();
		if (player != null) {
			player.targetWall = null;
		}
	}
	
}
