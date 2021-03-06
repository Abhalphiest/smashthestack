﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour {
    // Number of terrain pieces that can exist at once.
    private const int MAX_TERRAIN_PIECES = 8;
    protected const int NORMAL_PIECE_WIDTH = 8;
    protected const int WIDE_PIECE_WIDTH = 16;

    protected TerrainData[] terrain;
    public int leftIndex;
    public int rightIndex;
    protected int terrainCounter;

    // Public
    public GameObject[] Terrain_Prefabs;
    public GameObject Terrain_Flat;
    public GameObject Simon_Terrain;

    private bool quickStart; // controls number of starting flat platforms

    protected Queue<GameObject> simonQueue; 

    public int SimonSpacing = 25;

    protected TerrainData[] Terrain_Prefabs_Data;
    public float screenSpeed = 0.5f;
    protected GameObject player;
    public bool isPaused = false;
    protected Simon simon;

    public virtual void Start()
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
        terrain = new TerrainData[MAX_TERRAIN_PIECES];
        leftIndex = 0;

        if (FindObjectOfType<GlobalData>() != null)
        {
            quickStart = FindObjectOfType<GlobalData>().shortStart;
            print("shortstart true");
        }

        int flatAreaCount = quickStart ? 1 : 4;
        if (flatAreaCount == 1) print("flatAreaCount: " + flatAreaCount);

        simonQueue = new Queue<GameObject>();
        
        int xoffset = 0;
        // Spawn the starting terrain.
        for (int i = 0; i < MAX_TERRAIN_PIECES; ++i)
        {
            if (i < flatAreaCount)
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
                int myRandIndex = GetRandIndex();
                Terrain_Prefabs_Data[myRandIndex].freqMultiplier *= 0.1f;

                // Width of left piece
                xoffset += Terrain_Prefabs[myRandIndex].GetComponent<TerrainData>().isLarge ? WIDE_PIECE_WIDTH / 2 : NORMAL_PIECE_WIDTH / 2;
                GameObject newObject = Instantiate(Terrain_Prefabs[myRandIndex], new Vector3(xoffset, 0, 0), Quaternion.identity) as GameObject;
                terrain[i] = newObject.GetComponent<TerrainData>();
                terrainCounter++;
            }
            rightIndex = i;
            xoffset += terrain[rightIndex].isLarge ? WIDE_PIECE_WIDTH / 2 : NORMAL_PIECE_WIDTH / 2;

        }

        rightIndex = MAX_TERRAIN_PIECES - 1;
        
    }

	// Update is called once per frame
    public virtual void Update() {
        
        if (isPaused)
        {
            return;
        }

        // Before everything starts, bring up the weights.
        for (int i = 0; i < Terrain_Prefabs_Data.Length; ++i)
        {
            Terrain_Prefabs_Data[i].freqMultiplier += (1 - Terrain_Prefabs_Data[i].freqMultiplier) * 0.01f;
        }

        // First move all the objects.
        for (int i = 0; i < MAX_TERRAIN_PIECES; ++i)
        {
            terrain[i].transform.position = new Vector3(terrain[i].transform.position.x - screenSpeed, terrain[i].transform.position.y, terrain[i].transform.position.z);
        }

        int leftBound = terrain[leftIndex].isLarge ? -17*2 : -13*2;

        // then check if the leftmost object
        if (terrain[leftIndex].transform.position.x < leftBound)
        {
            Destroy(terrain[leftIndex].gameObject);

            terrain[leftIndex] = GenerateTerrainPiece();

            leftIndex = (leftIndex + 1) % MAX_TERRAIN_PIECES;
            rightIndex = (rightIndex + 1) % MAX_TERRAIN_PIECES;

        }
        if(simonQueue.Count > 0 && Mathf.Abs(simonQueue.Peek().transform.position.x - player.transform.position.x) < 1)
        {
            print("Set Pause");
            GetComponent<GameManagerScript>().StartSimon();
        }
	}

    private TerrainData GenerateTerrainPiece()
    {
        int myRandIndex = GetRandIndex();
        Terrain_Prefabs_Data[myRandIndex].freqMultiplier *= 0.1f;
        // Width of left piece
        int leftWidth = terrain[rightIndex].isLarge ? WIDE_PIECE_WIDTH/2: NORMAL_PIECE_WIDTH/2;

        GameObject newPiece;
        if (terrainCounter < SimonSpacing)
        {
            

            // Width of right piece (gonna put the random stuff in here)
            int rightWidth = Terrain_Prefabs[myRandIndex].GetComponent<TerrainData>().isLarge ? WIDE_PIECE_WIDTH/2 : NORMAL_PIECE_WIDTH/2;
            Vector3 position = terrain[rightIndex].transform.position + new Vector3(leftWidth + rightWidth, 0, 0);
            newPiece = Instantiate(Terrain_Prefabs[myRandIndex], position, Quaternion.identity) as GameObject;
            terrainCounter++;
        }
        else
        {
            Vector3 position = terrain[rightIndex].transform.position + new Vector3(leftWidth + WIDE_PIECE_WIDTH/2, 0, 0);
            terrainCounter = 0;
            newPiece = Instantiate(Simon_Terrain, position, Quaternion.identity) as GameObject;
            simonQueue.Enqueue(newPiece);
            if (simonQueue.Count == 1)
            {
                simon.Generate(simonQueue.Peek());
            }
        }
        return newPiece.GetComponent<TerrainData>();
    }
    public void CompletedSimon()
    {
        simonQueue.Dequeue();
        if (simonQueue.Count > 0)
        {
            simon.Generate(simonQueue.Peek());
        }
    }

    private int GetRandIndex()
    {
        float[] myWeights = new float[Terrain_Prefabs.Length];
        float weightCounter = 0.0f;

        for (int i = 0; i < Terrain_Prefabs.Length; ++i)
        {
            myWeights[i] = Terrain_Prefabs_Data[i].weight * Terrain_Prefabs_Data[i].freqMultiplier;
            weightCounter += myWeights[i];
        }

        float randomNum = Random.Range(0.0f, weightCounter);
        weightCounter = 0;
        for(int i = 0; i < Terrain_Prefabs_Data.Length; ++i){
            weightCounter += myWeights[i];

            if(weightCounter >= randomNum){

                return i;
            }
        }

        return 0;
    }

}
