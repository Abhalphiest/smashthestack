using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public Texture2D emptyProgressBar;
    public Texture2D fullProgressBar;

    private bool timerRunning = false;
    private float timerCount = 0;
    private float maxTimerCount = 10;

    //timer positioning
    private float timerWidth = 300;
    private float timerHeight = 50.0f;
    private float timerXPos = 0;
    private float timerYPos = 0;
    private float textAreaWidth = 50;

	void OnGUI()
    {
        GUI.DrawTexture(new Rect(timerXPos, timerYPos, timerWidth, timerHeight), emptyProgressBar);
        GUI.DrawTexture(new Rect(timerXPos, timerYPos, timerCount * timerWidth/maxTimerCount, timerHeight), fullProgressBar);
        //GUI.TextArea(new Rect(timerXPos + timerWidth/2 - textAreaWidth/2, timerYPos, textAreaWidth, timerHeight), (10 - (int)timerCount).ToString());
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
