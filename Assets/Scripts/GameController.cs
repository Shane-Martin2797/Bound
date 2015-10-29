﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : SingletonBehaviour<GameController>
{
	public static event System.Action<float> OnTimeChange;
	public List<GhostControl> GhostList = new List<GhostControl> ();
	public List<PushControl> HumanList = new List<PushControl> ();
	public int team1Score = 0;
	public int team2Score = 0;
	public int scoreLimit = 10;
	public float timer = 120;
	bool musicChanged = false;
	
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
		timer -= Time.deltaTime;
		if (OnTimeChange != null) {
			OnTimeChange (timer);
		}
		
		if (timer < 20) {
			if (!musicChanged) {
				BGMController.Instance.ChangeMusic (1);
				musicChanged = true;
			}
		}
		
		
		//This means that when the timer is at 0, the player will win if they are ahead of the other team, however if they are tied
		//The game will continue until one of the them scores.
		if (timer < 0) {
			if (team1Score > team2Score) {
				GameOver (1);
			} else if (team1Score < team2Score) {
				GameOver (2);
			}
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
