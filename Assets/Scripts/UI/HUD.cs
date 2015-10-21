using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Canvas))]
public class HUD : MonoBehaviour {

    private static Queue<PlayerController> playerLoadQueue = new Queue<PlayerController>();

    public static void LoadForPlayer(PlayerController player)
    {
        playerLoadQueue.Enqueue(player);
        Application.LoadLevelAdditive(Scenes.HUD);
    }

    void Awake()
    {
        if (playerLoadQueue.Count > 0)
        {
            var player = playerLoadQueue.Dequeue();
            name = "HUD (" + player.name + ")";
            var canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = player.uiCamera;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
