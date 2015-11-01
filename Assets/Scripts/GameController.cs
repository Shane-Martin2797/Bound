using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : SingletonBehaviour<GameController>
{
	public static event System.Action<float> OnTimeChange;
	public int team1Score = 0;
	public int team2Score = 0;
	public int scoreLimit = 100;
	
	public static int winTeam;
	
	protected override void OnSingletonAwake ()
	{
		Application.LoadLevelAdditive (Scenes.HUD);
	}
	
	void Start ()
	{
	}
	void Update ()
	{
		
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
