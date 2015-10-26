using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : SingletonBehaviour<GameController>
{
	public static event System.Action<float> OnTimeChange;
	public List<GhostControl> GhostList = new List<GhostControl> ();
	public List<PushControl> HumanList = new List<PushControl> ();
	public int[] Teams = new int[10];
	public float timer = 120;
	
	public static int winTeam;
	
	protected override void OnSingletonAwake ()
	{
		
	}
	
	void Start ()
	{
		SortTeams ();
	}
	void SortTeams ()
	{
		if (GameController.Instance.GhostList.Count > 0) {
			for (int i = 0; i < GameController.Instance.GhostList.Count; ++i) {
				Teams [GameController.Instance.GhostList [i].player.team] += 1;
			}
		}
		if (GameController.Instance.HumanList.Count > 0) {
			for (int i = 0; i < GameController.Instance.HumanList.Count; ++i) {
				Teams [GameController.Instance.HumanList [i].player.team] += 1;
			}
		}
		
		
	}
	void Update ()
	{
		timer -= Time.deltaTime;
		if (OnTimeChange != null) {
			OnTimeChange (timer);
		}
	}
	
	public void GameOver (int teamNumber)
	{
		winTeam = teamNumber;
		int wins = PlayerPrefs.GetInt ("Team" + teamNumber, 0);
		wins++;
		PlayerPrefs.SetInt ("Team" + teamNumber, wins);
		Application.LoadLevel (Scenes.EndScreen);
	}
}
