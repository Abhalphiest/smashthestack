using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour {
    // Number of terrain pieces that can exist at once.
    private const int MAX_TERRAIN_PIECES = 8;
    private const int NORMAL_PIECE_WIDTH = 8;
    private const int WIDE_PIECE_WIDTH = 16;

    private TerrainData[] terrain;
    public int leftIndex;
    public int rightIndex;
    private int terrainCounter;

    // Public
    public GameObject[] Terrain_Prefabs;
    public GameObject Terrain_Flat;
    public GameObject Simon_Terrain;

    private GameObject curr_Simon;
    public int SimonSpacing = 25;

    private TerrainData[] Terrain_Prefabs_Data;
    public float screenSpeed = 0.5f;
    private GameObject player;
    public bool isPaused = false;
    public bool success = false;
    private Simon simon;

    void Start()
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

        
        int xoffset = 0;
        // Spawn the starting terrain.
        for (int i = 0; i < MAX_TERRAIN_PIECES; ++i)
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

        int leftBound = terrain[leftIndex].isLarge ? -17*2 : -13*2;

        // then check if the leftmost object
        if (terrain[leftIndex].transform.position.x < leftBound)
        {
            Destroy(terrain[leftIndex].gameObject);

            terrain[leftIndex] = GenerateTerrainPiece();

            leftIndex = (leftIndex + 1) % MAX_TERRAIN_PIECES;
            rightIndex = (rightIndex + 1) % MAX_TERRAIN_PIECES;

        }
        if(curr_Simon != null && Mathf.Abs(curr_Simon.transform.position.x - player.transform.position.x) < 1 && !success)
        {
            print("try pause");
            GetComponent<GameManagerScript>().SetPause();
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
            Vector3 position = terrain[rightIndex].transform.position + new Vector3(leftWidth + NORMAL_PIECE_WIDTH/2, 0, 0);
            terrainCounter = 0;
            newPiece = Instantiate(Simon_Terrain, position, Quaternion.identity) as GameObject;
            curr_Simon = newPiece;
            simon.Generate();
            curr_Simon.transform.FindChild("NewLetter").GetComponent<SpriteRenderer>().sprite = simon.LastSprite;
            success = false;
        }
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
