using UnityEngine;
using System.Collections;

public class SplitScreenManager : MonoBehaviour {

    public Camera[] cameras;

    public Vector2 viewPortSize;
    public Vector2 viewPortPosition;

    void Awake()
    {
        if (cameras == null) return;

        //when we have a split screen mode, we want to resize all of the cameras for this player
        //because we have multiple cameras (chase camera, ui camera, etc), we can have one script
        //to make sure they all match the right viewport sizes
        foreach (var camera in cameras)
        {
            camera.rect = new Rect(viewPortPosition.x, viewPortPosition.y, viewPortSize.x, viewPortSize.y);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
}
