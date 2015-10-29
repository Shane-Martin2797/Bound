using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndZone : SingletonBehaviour<EndZone>
{
	public static List<PlayerController> finishedPlayers = new List<PlayerController> ();
	public Vector2 NegativeBounds = new Vector2 (-47, -70);
	public Vector2 PositiveBounds = new Vector2 (47, 10);
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (!col.isTrigger) {
			NotifyEnter (col.gameObject);
		}
	}
	void OnCollisionEnter2D (Collision2D col)
	{
		NotifyEnter (col.gameObject);
	}
	void OnTriggerExit2D (Collider2D col)
	{
		if (!col.isTrigger) {		
			NotifyExit (col.gameObject);
		}
	}
	void OnCollisionExit2D (Collision2D col)
	{
		NotifyExit (col.gameObject);
	}
	
	void NotifyEnter (GameObject gameObj)
	{
		PushControl player = gameObj.GetComponent<PushControl> ();
		if (player != null) {
			CheckWon (player);
			RepositionEndZone ();
		}
	}
	
	void NotifyExit (GameObject gameObj)
	{
		
	}

	
	void CheckWon (PushControl player)
	{
		int teamNumber = player.player.team;
		int score = 0;
		if (teamNumber == 1) {
			score = GameController.Instance.team1Score++;
		} else if (teamNumber == 2) {
			score = GameController.Instance.team2Score++;
		}
		if (score >= GameController.Instance.scoreLimit) {
			Debug.Log (teamNumber);
			GameController.Instance.GameOver (teamNumber);
		}
	}
	
	void RepositionEndZone ()
	{
		transform.position = new Vector3 (Random.Range (NegativeBounds.x, PositiveBounds.x + 1), Random.Range (NegativeBounds.y, PositiveBounds.y + 1), transform.position.z);
	}
}
