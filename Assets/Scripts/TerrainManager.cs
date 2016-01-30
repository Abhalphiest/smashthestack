using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour {
    // Number of terrain pieces that can exist at once.
    private const int MAX_TERRAIN_PIECES = 4;
    private const int NORMAL_PIECE_WIDTH = 4;
    private const int WIDE_PIECE_WIDTH = 8;

    private TerrainData[] terrain;
    public int leftIndex;
    public int rightIndex;

    // Public
    public GameObject[] Terrain_Prefabs;
    public float screenSpeed = 0.5f;

    public bool isPaused = false;

    void Start()
    {
        terrain = new TerrainData[MAX_TERRAIN_PIECES];
        leftIndex = 0;
        rightIndex = MAX_TERRAIN_PIECES - 1;

        for (int i = 0; i < MAX_TERRAIN_PIECES; ++i)
        {
            GameObject newObject = Instantiate(Terrain_Prefabs[0], new Vector3(-6 + i * WIDE_PIECE_WIDTH, 0, 0), Quaternion.identity) as GameObject;
            terrain[i] = newObject.GetComponent<TerrainData>();
        }
    }

	// Update is called once per frame
	void FixedUpdate() {

        if (isPaused)
        {
            return;
        }

        // First move all the objects.
        for (int i = 0; i < MAX_TERRAIN_PIECES; ++i)
        {
            terrain[i].transform.position = new Vector3(terrain[i].transform.position.x - screenSpeed, terrain[i].transform.position.y, terrain[i].transform.position.z);
        }

        int leftBound = terrain[leftIndex].isLarge ? -9 : -7;

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
        // Width of left piece
        int leftWidth = terrain[rightIndex].isLarge ? WIDE_PIECE_WIDTH / 2 :  NORMAL_PIECE_WIDTH / 2;

        // Width of right piece (gonna put the random stuff in here)
        int rightWidth = Terrain_Prefabs[0].GetComponent<TerrainData>().isLarge ? WIDE_PIECE_WIDTH / 2 : NORMAL_PIECE_WIDTH / 2;
       
        Vector3 position = terrain[rightIndex].transform.position + new Vector3(leftWidth + rightWidth, 0, 0);

        GameObject newPiece = Instantiate(Terrain_Prefabs[0], position, Quaternion.identity) as GameObject;

        return newPiece.GetComponent<TerrainData>();
    }
}
