using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManagerScript : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        simonList = new List<KeyCode>(); //for simon minigame
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}


    #region Simon

    List<int> simonList; //currently implemented with ints, can be easily substituted for Unity Color
    
    /// <summary>
    /// simon runs the memory minigame
    /// </summary>
    /// <param name="p_seconds">The number of seconds that the player has to complete the sequence.</param>
    /// <returns>A boolean indicating success or failure.</returns>
    bool simon(float p_seconds)
    {
        bool complete = false; //have they finished?
        float time = 0.0f; //the time elapsed
        int listIndex; //index of what color we're on
        while(time < p_seconds)
        {
            if(listIndex < simonList.Count)
            {

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
