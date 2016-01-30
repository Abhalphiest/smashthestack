using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManagerScript : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        simonList = new List<int>(); //for simon minigame
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}


    #region Simon


    List<int> simonList; //currently implemented with ints, can be easily substituted for Unity Color
    Dictionary<int, KeyCode> keystrokeMap; //to get what keystroke is associated with each color
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
                //display current color

                //if correct key
                if (Input.GetKeyDown(keystrokeMap[simonList[listIndex]]))
                {
                    listIndex++;
                }
                //else
                else
                {
                    listIndex = 0;
                }

            }
            else
            {
                complete = true;
                break; //better to break out than add a second check to the while loop
            }


        }

        return complete;
    }
    #endregion
}
