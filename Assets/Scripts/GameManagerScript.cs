using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManagerScript : MonoBehaviour {

    private bool paused = false;
    private GameObject player;
    private Simon simon;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        simon = GetComponent<Simon>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
    }
    
    void Update()
    {
       
    }
    public void SetSuccess(bool p_successValue)
    {
        if(!p_successValue)
        {
            //game over logic
        }
        else
        {
            print("success attempt");
            paused = false;
            player.GetComponent<RunnerBehavior>().Paused = false;
            GetComponent<TerrainManager>().success = true;
            GetComponent<TerrainManager>().isPaused = false;
        }
    }
    public void SetPause()
    {
        
            paused = true;
            GetComponent<Simon>().startSimon(10.0f);
            player.GetComponent<RunnerBehavior>().Paused = true;
            GetComponent<TerrainManager>().isPaused = true;

        
    }
   
}
