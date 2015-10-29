using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITimer : MonoBehaviour {

	public Text text;

	void OnEnable()
	{
		GameController.OnTimeChange += HandleOnTimeChange;
	}

	void OnDisable()
	{
		GameController.OnTimeChange -= HandleOnTimeChange;
	}

	void HandleOnTimeChange (float time)
	{
		text.text = time.ToString("0");
	}

}
