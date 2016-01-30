using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

public class Simon : MonoBehaviour {

   
    #region Color Index Definitions
    public const int BLUE_INDEX = 0;
    public const int RED_INDEX = 1;
    public const int YELLOW_INDEX = 2;
    public const int GREEN_INDEX = 3;
    public const int MAX_INDEX = 4;
    public const int WHITE_INDEX = 4;
    public const int BLACK_INDEX = 5;
    #endregion

    public Sprite[] Sprites;

    List<int> simonList; //currently implemented with ints, can be easily substituted for Unity Color. Only accept [0,5] integers.
    Dictionary<int, KeyCode> keystrokeMap; //to get what keystroke is associated with each color
    Color[] colorArr;
    int simonListIndex = 0; //index of what color we're on
    float seconds = 0.0f;
    /// <summary>
    /// simon runs the memory minigame
    /// </summary>
    
    public Sprite LastSprite
    {
        get { return Sprites[simonList.Last()]; } }

    int simon()
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
                return 0;
            }
            else
            {
                simonListIndex = 0;
                seconds = 0.0f;
                return 1;
            }
            seconds -= Time.deltaTime; //increment our time clock
            
        }
        return -1;

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
    public void pushSimonColor(int p_colorIndex)
    {
        if (p_colorIndex >= 0 && p_colorIndex < 6)
            simonList.Add(p_colorIndex);
    }
    // Use this for initialization
    void Start () {

        simonList = new List<int>(); //for simon minigame
        colorArr = new Color[6];
        colorArr[BLUE_INDEX] = new Color(.125f, .125f, 1.0f);//BLUE
        colorArr[RED_INDEX] = new Color(.929f, .125f, .125f);//RED
        colorArr[YELLOW_INDEX] = new Color(1.0f, .949f, 0.0f); //YELLOW
        colorArr[GREEN_INDEX] = new Color(.058f, .521f, .0156f); //GREEN
        colorArr[WHITE_INDEX] = new Color(1.0f, 1.0f, 1.0f); //WHITE
        colorArr[BLACK_INDEX] = new Color(0, 0, 0); //BLACK
        keystrokeMap = new Dictionary<int, KeyCode>();
        keystrokeMap[BLUE_INDEX] = KeyCode.H;
        keystrokeMap[RED_INDEX] = KeyCode.J;
        keystrokeMap[YELLOW_INDEX] = KeyCode.K;
        keystrokeMap[GREEN_INDEX] = KeyCode.L;
        keystrokeMap[WHITE_INDEX] = KeyCode.F;
        keystrokeMap[BLACK_INDEX] = KeyCode.G;
        Camera.main.clearFlags = CameraClearFlags.Color;
    }
	
	// Update is called once per frame
	void Update () {
        int result;
        if (seconds > 0.0f)
        {
            result = simon();
            if (result == 1)
            {
                print("won");
                GetComponent<GameManagerScript>().SetSuccess(true);
            }
            else if(result == -1)
            {
                print("lost");
                GetComponent<GameManagerScript>().SetSuccess(false);
            }

        }
        
    }

    public void startSimon(float p_seconds)
    {
        seconds = p_seconds;
    }

    public void Generate()
    {
        pushSimonColor(Random.Range(0, MAX_INDEX));
    }
}
