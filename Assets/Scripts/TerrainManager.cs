using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour {
    // Number of terrain pieces that can exist at once.
    private const int maxTerrainPieces = 4;

    private TerrainData[] terrain;
    public int leftIndex;
    public int rightIndex;

    // Public
    public GameObject[] Terrain_Prefabs;
    public float screenSpeed = 0.5f;

    public bool isPaused = false;

    void Start()
    {
        terrain = new TerrainData[maxTerrainPieces];
        leftIndex = 0;
        rightIndex = maxTerrainPieces - 1;

        for (int i = 0; i < maxTerrainPieces; ++i)
        {
            GameObject newObject = Instantiate(Terrain_Prefabs[0], new Vector3(-6 + i * 8, 0, 0), Quaternion.identity) as GameObject;
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
        for (int i = 0; i < maxTerrainPieces; ++i)
        {
            terrain[i].transform.position = new Vector3(terrain[i].transform.position.x - screenSpeed, terrain[i].transform.position.y, terrain[i].transform.position.z);
        }

        int leftBound = terrain[leftIndex].isLarge ? -9 : -7;

        // then check if the leftmost object
        if (terrain[leftIndex].transform.position.x < leftBound)
        {
            Destroy(terrain[leftIndex].gameObject);

            terrain[leftIndex] = GenerateTerrainPiece();

            leftIndex = (leftIndex + 1) % maxTerrainPieces;
            rightIndex = (rightIndex + 1) % maxTerrainPieces;

        }
	}

    private TerrainData GenerateTerrainPiece()
    {
        // Width of left piece
        int leftWidth = terrain[rightIndex].isLarge ? 4 : 2;

        // Width of right piece (gonna put the random stuff in here)
        int rightWidth = Terrain_Prefabs[0].GetComponent<TerrainData>().isLarge ? 4 : 2;
       
        Vector3 position = terrain[rightIndex].transform.position + new Vector3(leftWidth + rightWidth, 0, 0);

        GameObject newPiece = Instantiate(Terrain_Prefabs[0], position, Quaternion.identity) as GameObject;

        return newPiece.GetComponent<TerrainData>();
    }
}
