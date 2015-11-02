using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : SingletonBehaviour<GameController>
{
	public static event System.Action<float> OnTimeChange;
	public static event System.Action<int> OnTeam1LiveChange;
	public static event System.Action<int> OnTeam2LiveChange;
	public int team1Lives = 6;
	public int team2Lives = 6;
	public int scoreLimit = 100;
	public float timer = 120;
	
	public static int winTeam;
	
	
	
	void Update ()
	{
		timer -= Time.deltaTime;
		if (OnTimeChange != null) {
			OnTimeChange (timer);
		}
	}
	protected override void OnSingletonAwake ()
	{
		Application.LoadLevelAdditive (Scenes.HUD);
	}
	
	public void Team1LosesLife ()
	{
		team1Lives--;
		if (OnTeam1LiveChange != null) {
			OnTeam1LiveChange (team1Lives);
		}
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
		Application.LoadLevel (Scenes.EndScreen);
	}
}
