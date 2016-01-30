using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Simon : MonoBehaviour {

   
    #region Color Index Definitions
    const int BLUE_INDEX = 0;
    const int RED_INDEX = 1;
    const int YELLOW_INDEX = 2;
    const int GREEN_INDEX = 3;
    const int WHITE_INDEX = 4;
    const int BLACK_INDEX = 5;
    #endregion

    List<int> simonList; //currently implemented with ints, can be easily substituted for Unity Color. Only accept [0,5] integers.
    Dictionary<int, KeyCode> keystrokeMap; //to get what keystroke is associated with each color
    Color[] colorArr;
    int simonListIndex = 0; //index of what color we're on
    float seconds = 0.0f;
    /// <summary>
    /// simon runs the memory minigame
    /// </summary>
    
    bool simon()
    {
        if (seconds > 0)
        {
            if (simonListIndex < simonList.Count)
            {
                if (Input.anyKeyDown)
                {
                    //if correct key
                    if (Input.GetKeyDown(keystrokeMap[simonList[simonListIndex]]))
                    {
                        print("correct");
                        //display color
                        flashColor(simonList[simonListIndex]);
                        simonListIndex++;
                    }
                    //else
                    else
                    {
                        print("wrong");
                        //let them know it reset
                        flashError();
                        simonListIndex = 0;
                    }

                }
            }
            else
            {
                simonListIndex = 0;
                seconds = 0.0f;
                return true;
            }
            seconds -= Time.deltaTime; //increment our time clock
            
        }
        return false;

    }
    void flashColor(int p_colorIndex)
    {
        //to be changed later, obviously
        if (p_colorIndex >= 0 && p_colorIndex < 6)
        {
            print(colorArr[p_colorIndex]);
           
            Camera.main.backgroundColor = colorArr[p_colorIndex];
        }
    }
    void flashError()
    {
        //do whatever we're doing in response to errors
    }
    void pushSimonColor(int p_colorIndex)
    {
        if (p_colorIndex >= 0 && p_colorIndex < 6)
            simonList.Add(p_colorIndex);
    }
    // Use this for initialization
    void Start () {

        simonList = new List<int>(); //for simon minigame
        colorArr = new Color[6];
        colorArr[BLUE_INDEX] = new Color(32, 32, 255);//BLUE
        colorArr[RED_INDEX] = new Color(237, 32, 32);//RED
        colorArr[YELLOW_INDEX] = new Color(255, 242, 0); //YELLOW
        colorArr[GREEN_INDEX] = new Color(15, 133, 4); //GREEN
        colorArr[WHITE_INDEX] = new Color(255, 255, 255); //WHITE
        colorArr[BLACK_INDEX] = new Color(0, 0, 0); //BLACK
        keystrokeMap = new Dictionary<int, KeyCode>();
        keystrokeMap[BLUE_INDEX] = KeyCode.Alpha1;
        keystrokeMap[RED_INDEX] = KeyCode.Alpha2;
        keystrokeMap[YELLOW_INDEX] = KeyCode.Alpha3;
        keystrokeMap[GREEN_INDEX] = KeyCode.Alpha4;
        keystrokeMap[WHITE_INDEX] = KeyCode.Alpha5;
        keystrokeMap[BLACK_INDEX] = KeyCode.Alpha6;
        Camera.main.clearFlags = CameraClearFlags.Color;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("caught simon call");
            seconds = 30.0f;
            
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {

            print("pushing red");
            pushSimonColor(RED_INDEX);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {

            print("pushing blue");
            pushSimonColor(BLUE_INDEX);
        }

        if(seconds > 0.0f)
        {
            bool result = simon();
            if(result)
                print(result);
        }
    }
}
