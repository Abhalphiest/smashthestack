using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManagerScript : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        simonList = new List<int>(); //for simon minigame
        colorArr = new Color[6];
        colorArr[0] = new Color(32, 32, 255);//BLUE
        colorArr[1] = new Color(237, 32, 32);//RED
        colorArr[2] = new Color(255, 242, 0); //YELLOW
        colorArr[3] = new Color(15, 133, 4); //GREEN
        colorArr[4] = new Color(255, 255, 255); //WHITE
        colorArr[5] = new Color(0, 0, 0); //BLACK
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    
	}


    #region Simon

    List<int> simonList; //currently implemented with ints, can be easily substituted for Unity Color. Only accept [0,5] integers.
    Dictionary<int, KeyCode> keystrokeMap; //to get what keystroke is associated with each color
    Color[] colorArr;
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
        while(time < p_seconds)
        {
           


            if(listIndex < simonList.Count)
            {
                
                //if correct key
                if (Input.GetKeyDown(keystrokeMap[simonList[listIndex]]))
                {
                    //display color
                    flashColor(simonList[listIndex]);
                    listIndex++;
                }
                //else
                else
                {
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

            time += Time.deltaTime; //increment our time clock
        }

        return complete; //did they complete it?
    }
    void flashColor(int p_colorIndex)
    {
        //to be changed later, obviously
        if(p_colorIndex >=0 && p_colorIndex < 6)
            GetComponent<Camera>().backgroundColor = colorArr[p_colorIndex];
    }
    void flashError()
    {
        //do whatever we're doing in response to errors
    }
    #endregion
}
