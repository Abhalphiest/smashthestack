using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManagerScript : MonoBehaviour {

    #region Color Index Definitions
    const int BLUE_INDEX = 0;
    const int RED_INDEX = 1;
    const int YELLOW_INDEX = 2;
    const int GREEN_INDEX = 3;
    const int WHITE_INDEX = 4;
    const int BLACK_INDEX = 5;
    #endregion

    // Use this for initialization
    void Start () {
        initSimon();
        

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.S))
        {

            Debug.Log(simon(30.0f));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {

            Debug.Log("pushing red");
            pushSimonColor(RED_INDEX);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {

            Debug.Log("pushing blue");
            pushSimonColor(BLUE_INDEX);
        }
    }
    
    #region Simon

    List<int> simonList; //currently implemented with ints, can be easily substituted for Unity Color. Only accept [0,5] integers.
    Dictionary<int, KeyCode> keystrokeMap; //to get what keystroke is associated with each color
    Color[] colorArr;
    void initSimon()
    {
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
    }
 
    /// <summary>
    /// simon runs the memory minigame
    /// </summary>
    /// <param name="p_seconds">The number of seconds that the player has to complete the sequence.</param>
    /// <returns>A boolean indicating success or failure.</returns>
    bool simon(float p_seconds)
    {
        bool complete = false; //have they finished?
        float time = 0.0f; //the time elapsed
        int listIndex = 0; //index of what color we're on
        Debug.Log("starting simon");
        while(time < p_seconds)
        {



            if (listIndex < simonList.Count)
            {
                if (Input.anyKey)
                {
                    //if correct key
                    if (Input.GetKeyDown(keystrokeMap[simonList[listIndex]]))
                    {
                        Debug.Log("correct");
                        //display color
                        flashColor(simonList[listIndex]);
                        listIndex++;
                    }
                    //else
                    else
                    {
                        Debug.Log("wrong");
                        //let them know it reset
                        flashError();
                        listIndex = 0;
                    }

                }
                else
                {
                    complete = true;
                    break; //better to break out than add a second check to the while loop
                }
            }
            time += Time.deltaTime; //increment our time clock
        }

        return complete; //did they complete it?
    }
    void flashColor(int p_colorIndex)
    {
        //to be changed later, obviously
        if (p_colorIndex >= 0 && p_colorIndex < 6)
            GetComponent<Camera>().backgroundColor = colorArr[p_colorIndex];
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
    #endregion
}
