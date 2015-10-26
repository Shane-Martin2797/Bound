using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndZone : MonoBehaviour
{
	public static List<PlayerController> finishedPlayers = new List<PlayerController> ();

	public int teamWin;
	
	void OnTriggerEnter2D (Collider2D col)
	{
		NotifyEnter (col.gameObject);
	}
	void OnCollisionEnter2D (Collision2D col)
	{
		NotifyEnter (col.gameObject);
	}
	void OnTriggerExit2D (Collider2D col)
	{
		NotifyExit (col.gameObject);
	}
	void OnCollisionExit2D (Collision2D col)
	{
		NotifyExit (col.gameObject);
	}
	
	void NotifyEnter (GameObject gameObj)
	{
		PlayerController player = gameObj.GetComponent<PlayerController> ();
		if (player != null) {
			finishedPlayers.Add (player);
			CheckWon (player);
		}
	}
	
	void NotifyExit (GameObject gameObj)
	{
		PlayerController player = gameObj.GetComponent<PlayerController> ();
		if (player != null) {
			finishedPlayers.Remove (player);
		}
		
	}

	
	void CheckWon (PlayerController player)
	{
		int teamNumber = player.team;
		if (teamNumber == teamWin) {
			int totalForTeam = GameController.Instance.Teams [teamNumber];
			for (int i = 0; i < finishedPlayers.Count; ++i) {
				if (finishedPlayers [i].team == teamNumber) {
					totalForTeam--;
				}
			}
			if (totalForTeam == 0) {
				GameController.Instance.GameOver (teamNumber);
			}
		}
	}
}
