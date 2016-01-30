using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour {
    // Number of terrain pieces that can exist at once.
    private const int MAX_TERRAIN_PIECES = 4;
    private const int NORMAL_PIECE_WIDTH = 8;
    private const int WIDE_PIECE_WIDTH = 16;

    private TerrainData[] terrain;
    public int leftIndex;
    public int rightIndex;

    // Public
    public GameObject[] Terrain_Prefabs;
    public GameObject Terrain_Flat;
    public GameObject[] Terrain_Simons;

    private TerrainData[] Terrain_Prefabs_Data;
    public float screenSpeed = 0.5f;

    public bool isPaused = false;

    void Start()
    {
        // Setup the prefabs data
        Terrain_Prefabs_Data = new TerrainData[Terrain_Prefabs.Length];
        for (int i = 0; i < Terrain_Prefabs_Data.Length; ++i)
        {
            Terrain_Prefabs_Data[i] = Terrain_Prefabs[i].GetComponent<TerrainData>();
        }

        // Initialize Data
        terrain = new TerrainData[MAX_TERRAIN_PIECES];
        leftIndex = 0;
        rightIndex = MAX_TERRAIN_PIECES - 1;

        // Spawn the starting terrain.
        for (int i = 0; i < MAX_TERRAIN_PIECES; ++i)
        {
            GameObject newObject = Instantiate(Terrain_Flat, new Vector3(-WIDE_PIECE_WIDTH + i * WIDE_PIECE_WIDTH, 0, 0), Quaternion.identity) as GameObject;
            terrain[i] = newObject.GetComponent<TerrainData>();
        }
    }

	// Update is called once per frame
	void Update() {
        
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

        int leftBound = terrain[leftIndex].isLarge ? -17 : -13;

        // then check if the leftmost object
        if (terrain[leftIndex].transform.position.x < leftBound)
        {
            Destroy(terrain[leftIndex].gameObject);

            terrain[leftIndex] = GenerateTerrainPiece();

            leftIndex = (leftIndex + 1) % MAX_TERRAIN_PIECES;
            rightIndex = (rightIndex + 1) % MAX_TERRAIN_PIECES;

        }
	}

    private TerrainData GenerateTerrainPiece()
    {
        int myRandIndex = GetRandIndex();
        Terrain_Prefabs_Data[myRandIndex].freqMultiplier *= 0.1f;

        // Width of left piece
        int leftWidth = terrain[rightIndex].isLarge ? WIDE_PIECE_WIDTH / 2 :  NORMAL_PIECE_WIDTH / 2;

        // Width of right piece (gonna put the random stuff in here)
        int rightWidth = Terrain_Prefabs[myRandIndex].GetComponent<TerrainData>().isLarge ? WIDE_PIECE_WIDTH / 2 : NORMAL_PIECE_WIDTH / 2;
       
        Vector3 position = terrain[rightIndex].transform.position + new Vector3(leftWidth + rightWidth, 0, 0);

        GameObject newPiece = Instantiate(Terrain_Prefabs[myRandIndex], position, Quaternion.identity) as GameObject;

        return newPiece.GetComponent<TerrainData>();
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
