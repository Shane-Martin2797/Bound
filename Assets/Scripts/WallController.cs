using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour
{
	public float wallSpeed = 10;
	
	void OnCollisionStay (Collision col)
	{
		Notify (col.gameObject);
	}
	void Notify (GameObject gameObj)
	{
		PlayerController player = gameObj.GetComponent<PlayerController> ();
		if (player != null) {
			WallMove (player);
		}
	}
	
	void WallMove (PlayerController player)
	{
		Vector3 direction = (transform.position - player.transform.position).normalized;
		direction.y = 0;
		direction.x = 0;
		
		transform.Translate (direction * Time.deltaTime * wallSpeed);
	}
	
}
