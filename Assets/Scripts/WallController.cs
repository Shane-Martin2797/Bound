using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour
{	
	void OnCollisionEnter2D (Collision2D col)
	{
		NotifyEnter (col.gameObject);
	}
	void OnCollisionExit2D (Collision2D col)
	{
		NotifyExit (col.gameObject);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		NotifyEnter (col.gameObject);
	}
	void OnTriggerExit2D (Collider2D col)
	{
		NotifyExit (col.gameObject);
	}
	
	void NotifyEnter (GameObject gameObj)
	{
		PlayerController player = gameObj.GetComponent<PlayerController> ();
		if (player != null) {
		}
	}
	void NotifyExit (GameObject gameObj)
	{
		PlayerController player = gameObj.GetComponent<PlayerController> ();
		if (player != null) {
		}
	}
	
}
