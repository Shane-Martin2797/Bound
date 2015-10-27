using UnityEngine;
using System.Collections;

public class PushWallController : MonoBehaviour
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
		PushControl player = gameObj.GetComponent<PushControl> ();
		if (player != null) {
			player.targetedWall = this;
		} 
	}
	void NotifyExit (GameObject gameObj)
	{
		PushControl player = gameObj.GetComponent<PushControl> ();
		if (player != null) {
			player.targetedWall = null;
		} 
	}
	
	public void Move (Vector2 input, float speed)
	{
		float speedM = 0;
		if (transform.localEulerAngles.z == 90 || transform.localEulerAngles.z == 270) {
			speedM = input.x;
		} else if (transform.localEulerAngles.z == 0 || transform.localEulerAngles.z == 180 || transform.localEulerAngles.z == 360) {
			speedM = input.y;
		}
		
		transform.Translate (Vector3.up * Time.deltaTime * speed * speedM);
		
	}
	
}
