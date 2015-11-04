using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILives : MonoBehaviour
{
	public Text team1Lives;
	public Text team2Lives;
	
	void OnEnable ()
	{
		GameController.OnTeam1LiveChange += HandleOnTeam1LiveChange;
		GameController.OnTeam2LiveChange += HandleOnTeam2LiveChange;
	}
	void OnDisable ()
	{
		GameController.OnTeam1LiveChange -= HandleOnTeam1LiveChange;
		GameController.OnTeam2LiveChange -= HandleOnTeam2LiveChange;
	}
	
	void HandleOnTeam1LiveChange (int num)
	{
		team1Lives.text = "x" + num.ToString ("0");
	}

	void HandleOnTeam2LiveChange (int num)
	{
		team2Lives.text = "x" + num.ToString ("0");
	}
}
