using UnityEngine;
using System.Collections;

public class PushWallController : MonoBehaviour
{	

	public PushControl playerC;
	
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
		PushControl player = gameObj.GetComponent<PushControl> ();
		if (player != null) {
			playerC = player;
			player.targetedWall = this;
		} else if (playerC != null) {
			playerC.DropWall ();
		}
	}
	void NotifyExit (GameObject gameObj)
	{
		PushControl player = gameObj.GetComponent<PushControl> ();
		if (player != null) {
			playerC = null;
			player.targetedWall = null;
		} 
	}
	
	public void Move (Vector2 input, float speed)
	{
		float speedM = 0;
		if ((transform.localEulerAngles.z >= 45 && transform.localEulerAngles.z <= 135) || (transform.localEulerAngles.z >= 225 && transform.localEulerAngles.z <= 315)) {
			speedM = -input.x;
		} else if ((transform.localEulerAngles.z < 45) || (transform.localEulerAngles.z > 135 && transform.localEulerAngles.z < 225) || (transform.localEulerAngles.z > 315)) {
			speedM = input.y;
		}
		
		transform.Translate (Vector3.up * Time.deltaTime * speed * speedM);
		
	}
	
}
