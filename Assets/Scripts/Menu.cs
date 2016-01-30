using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
    public Texture2D MenuImage;
    
    // GUI
    void OnGUI () {
        GUI.Box(new Rect(0, 0, 1920, 1080), MenuImage);
        Debug.Log(Application.loadedLevel);

        if (Application.loadedLevelName == "MenuTest")
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Play Level 2"))
            {
                Application.LoadLevel("LevelLoadTest");
            }
        }
        if (Application.loadedLevelName == "LevelLoadTest")
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Play Level 1"))
            {
                Application.LoadLevel("MenuTest");
            }
        }
    }

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
	
	}
}
