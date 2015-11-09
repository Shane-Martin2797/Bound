using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : SingletonBehaviour<GameController>
{
	public static event System.Action<float> OnTimeChange;
	public static event System.Action<int> OnTeam1LiveChange;
	public static event System.Action<int> OnTeam2LiveChange;
	public PlayerController[] players;
	public int team1Lives = 6;
	public int team2Lives = 6;
	public int scoreLimit = 100;
	public float timer = 120;
	
	public static int winTeam;
	public Camera cam;
	public GameObject inCont;
	
	void Update ()
	{
		if (timer > 0) {
			timer -= Time.deltaTime;
			if (OnTimeChange != null) {
				OnTimeChange (timer);
			}
		} else {
			if (team1Lives > team2Lives) {
				GameOver (1);
			} else if (team2Lives > team1Lives) {
				GameOver (2);
			}
		}
	}
	protected override void OnSingletonAwake ()
	{
		Application.LoadLevelAdditive (Scenes.HUD);
		players = FindObjectsOfType<PlayerController> ();
		foreach (var player in players) {
			player.Respawn ();
		}
		cam = FindObjectOfType<Camera> ();
		inCont = FindObjectOfType<InControl.InControlManager> ().gameObject;
	}
	
	public void Team1LosesLife ()
	{
		team1Lives--;
		if (OnTeam1LiveChange != null) {
			OnTeam1LiveChange (team1Lives);
		}
		Debug.Log ("Team1LosesLife " + team1Lives);
		if (team1Lives <= 0) {
			GameOver (2);
		}
	}
	public void Team2LosesLife ()
	{
		team2Lives--;
		if (OnTeam2LiveChange != null) {
			OnTeam2LiveChange (team2Lives);
		}
		if (team2Lives <= 0) {
			GameOver (1);
		}
	}
	
	public void GameOver (int teamNumber)
	{
		int wins = PlayerPrefs.GetInt ("Team" + teamNumber, 0);
		wins++;
		PlayerPrefs.SetInt ("Team" + teamNumber, wins);
		winTeam = teamNumber;
		
		if (teamNumber == 1) {
			Application.LoadLevel (Scenes.OrangeWin);
		}
		if (teamNumber == 2) {
			Application.LoadLevel (Scenes.BlueWin);
		}
		for (int i = 0; i < players.Length; ++i) {
			Destroy (players [i].gameObject);
		}
		Destroy (cam.gameObject);
		Destroy (inCont.gameObject);
		Destroy (gameObject);
	}
}
