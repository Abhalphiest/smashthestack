using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public Texture2D emptyProgressBar;
    public Texture2D fullProgressBar;

    private float timerCount = 0;
    private float maxTimerCount = 10;

    //timer positioning
    private float timerWidth = 300;
    private float timerHeight = 50.0f;
    private float timerXPos = 0;
    private float timerYPos = 0;

	void OnGUI()
    {
        GUI.DrawTexture(new Rect(timerXPos, timerYPos, timerWidth, timerHeight), emptyProgressBar);
        GUI.DrawTexture(new Rect(timerXPos, timerYPos, timerCount * timerWidth/maxTimerCount, timerHeight), fullProgressBar);
    }

    void Update()
    {
        if (timerCount < maxTimerCount)
        {
            timerCount += Time.deltaTime;
        }
        else
        {
            Debug.Log("YOU LOSE");
        }
    }
}
