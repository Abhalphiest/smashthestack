using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialTerrainManager : TerrainManager
{
    private const int LENGTH = 20;
    private const int JUMP = 5;
    private const int SLIDE = 7;
    private const int SIMON1 = 9;
    private const int SIMON2 = 11;
    private const int SIMON3 = 13;
    private const int END = 15;

    private bool simonQueued;

    private Queue<TutorialStep> tutorialQueue;

    public Texture2D popUpMessage;

    public override void Start()
    {
        simon = GetComponent<Simon>();
        player = GameObject.FindGameObjectWithTag("Player");
        terrainCounter = 0;
        // Setup the prefabs data
        Terrain_Prefabs_Data = new TerrainData[Terrain_Prefabs.Length];
        for (int i = 0; i < Terrain_Prefabs_Data.Length; ++i)
        {
            Terrain_Prefabs_Data[i] = Terrain_Prefabs[i].GetComponent<TerrainData>();
        }

        // Initialize Data
        terrain = new TerrainData[LENGTH];
        leftIndex = 0;

        simonQueue = new Queue<GameObject>();
        
        int xoffset = 0;
        // Spawn the starting terrain.
        for (int i = 0; i < LENGTH; ++i)
        {
            if (i < 4)
            {
                if (i != 0)
                {
                    xoffset += Terrain_Flat.GetComponent<TerrainData>().isLarge ? WIDE_PIECE_WIDTH / 2 : NORMAL_PIECE_WIDTH / 2;
                }
                GameObject newObject = Instantiate(Terrain_Flat, new Vector3(xoffset, 0, 0), Quaternion.identity) as GameObject;
                terrain[i] = newObject.GetComponent<TerrainData>();
                terrainCounter++;
            }
            else
            {
                int myRandIndex = GetPlatformID(i);

                // Width of left piece
                xoffset += Terrain_Prefabs[myRandIndex].GetComponent<TerrainData>().isLarge ? WIDE_PIECE_WIDTH / 2 : NORMAL_PIECE_WIDTH / 2;
                GameObject newObject = Instantiate(Terrain_Prefabs[myRandIndex], new Vector3(xoffset, 0, 0), Quaternion.identity) as GameObject;
                terrain[i] = newObject.GetComponent<TerrainData>();
                terrainCounter++;

                if (i == SIMON1 || i == SIMON2 || i == SIMON3)
                {
                    simonQueue.Enqueue(newObject);
                }
            }
            xoffset += terrain[rightIndex].isLarge ? WIDE_PIECE_WIDTH / 2 : NORMAL_PIECE_WIDTH / 2;

        }

        tutorialQueue= new Queue<TutorialStep>();
        tutorialQueue.Enqueue(new TutorialStep()
        {
            Index = JUMP,
            Message = "Press W to Jump"
        });
        tutorialQueue.Enqueue(new TutorialStep()
        {
            Index = SLIDE,
            Message = "Press S to Slide"
        });
        tutorialQueue.Enqueue(new TutorialStep()
        {
            Index = SIMON1,
            Message = "Press the key on the golden platform"
        });
        tutorialQueue.Enqueue(new TutorialStep()
        {
            Index = SIMON2,
            Message = "Press the key from the first platform, then the key on this platform"
        });
        tutorialQueue.Enqueue(new TutorialStep()
        {
            Index = SIMON3,
            Message = "Enter the complete platform sequence"
        });
        tutorialQueue.Enqueue(new TutorialStep()
        {
            Index = END,
            Message = "Good luck!"
        });
    }

    private int GetPlatformID(int i)
    {
        if (i == JUMP)
        {
            return 8;
        }
        if (i == SLIDE)
        {
            return 6;
        }
        if (i == SIMON1 || i == SIMON2 || i == SIMON3)
        {
            return 15;
        }
        return 9;
    }

    // Update is called once per frame
    public override void Update() {
        
        if (isPaused)
        {
            return;
        }

        if (!simonQueued)
        {
            for (int i = 0; i < LENGTH; ++i)
            {
                if (i == SIMON1 || i == SIMON2 || i == SIMON3)
                {
                    simonQueue.Enqueue(terrain[i].gameObject);
                }
            }
            simon.Generate(simonQueue.Peek());
            simonQueued = true;
        }

        // First move all the objects.
        for (int i = 0; i < LENGTH; ++i)
        {
            terrain[i].transform.position = new Vector3(terrain[i].transform.position.x - screenSpeed, terrain[i].transform.position.y, terrain[i].transform.position.z);
        }

        if (simonQueue.Count > 0 && Mathf.Abs(simonQueue.Peek().transform.position.x - player.transform.position.x) < 1)
        {
            print("Set Pause");
            GetComponent<GameManagerScript>().StartSimon();
        }

        if (tutorialQueue.Count <= 0)
        {
            Application.LoadLevel(0);
        }

        if (tutorialQueue.Count > 0 && Mathf.Abs(terrain[tutorialQueue.Peek().Index - 1].transform.position.x - player.transform.position.x) < 1)
        {
            StartCoroutine(TutorialSegment(tutorialQueue.Peek().Message));
        }

    }

    private IEnumerator TutorialSegment(string message)
    {
        GetComponent<GameManagerScript>().SetPause();
        tutorialQueue.Dequeue();
        //Set Message
        Debug.Log(message);
        yield return new WaitForSeconds(2);
        GetComponent<GameManagerScript>().UnPause();
    }
}

internal class TutorialStep
{
    public int Index;
    public string Message;
}
