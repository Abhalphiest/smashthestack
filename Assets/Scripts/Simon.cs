﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine.UI;

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

    //textures for totem
    public Sprite[] Sprites;
    //sound effects for feedback
    public AudioClip[] feedback; // 0=F, 1=G, 2=H=Blue, 3=J=Red, 4=K=Yellow, 5=L=Green
    new AudioSource audio;

    List<int> simonList; //currently implemented with ints, can be easily substituted for Unity Color. Only accept [0,5] integers.
    public List<int> SimonList { get { return simonList} };
    Dictionary<int, KeyCode> keystrokeMap; //to get what keystroke is associated with each color
    Color[] colorArr;
    int simonListIndex = 0; //index of what color we're on
    float seconds = 0.0f;
    private float maxTimerCount; //seconds you start with
    private Slider progressSlider;
    private HorizontalLayoutGroup recordContent;

    public GameObject SimonRecordPrefab;

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
                        GameObject record = Instantiate(SimonRecordPrefab);
                        record.GetComponent<Image>().sprite = Sprites[simonList[simonListIndex]];
                        record.transform.parent = recordContent.transform;
                        record.transform.localScale = Vector3.one;
                        if (simonListIndex > 19)
                        {
                            recordContent.padding.left = -40*(simonListIndex - 19);
                        }
                        simonListIndex++;
                    }
                    //else
                    else
                    {
                        print("wrong");
                        //let them know it reset
                        flashError();
                        while (recordContent.transform.childCount > 0)
                        {
                            Destroy(recordContent.transform.GetChild(0));
                        }
                        simonListIndex = 0;
                    }
                }
                seconds -= Time.deltaTime; //increment our time clock
                return 0;
            }
            else
            {
                simonListIndex = 0;
                seconds = 0.0f;
                running = false;
                return 1;
            }
        }
        running = false;
        return -1;
    }
    void flashColor(int p_colorIndex)
    {
        // 0=F, 1=G, 2=H=Blue, 3=J=Red, 4=K=Yellow, 5=L=Green

        //to be changed later, obviously
        if (p_colorIndex >= 0 && p_colorIndex < 6)
        {
            print(colorArr[p_colorIndex]);
           
            Camera.main.backgroundColor = colorArr[p_colorIndex];

            switch (p_colorIndex)
            {
                case 0: //Blue, H, Audio File 2
                    audio.PlayOneShot(feedback[2]);
                    break;
                case 1: //Red, J, Audio File 3
                    audio.PlayOneShot(feedback[3]);
                    break;
                case 2: //Yellow, K, Audio File 4
                    audio.PlayOneShot(feedback[4]);
                    break;
                case 3: //Green, L, Audio File5
                    audio.PlayOneShot(feedback[5]);
                    break;
            }
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
        keystrokeMap[WHITE_INDEX] = KeyCode.U;
        keystrokeMap[BLACK_INDEX] = KeyCode.I;
        Camera.main.clearFlags = CameraClearFlags.Color;

        audio = GetComponent<AudioSource>();

    }

    private bool running = false;
	// Update is called once per frame
	void Update () {
        int result;
        if (running)
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
	    progressSlider.value = seconds/maxTimerCount;

        }
	}

    public void startSimon(float p_seconds)
    {
        seconds = p_seconds;
        maxTimerCount = p_seconds;
        running = true;
    }

    public void Generate(GameObject currSimon)
    {
        pushSimonColor(Random.Range(0, MAX_INDEX));
        currSimon.transform.FindChild("NewLetter").GetComponent<SpriteRenderer>().sprite = LastSprite;

        progressSlider = currSimon.GetComponentInChildren<Slider>();
        recordContent = currSimon.GetComponentInChildren<HorizontalLayoutGroup>();
    }
}
