using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManagerScript : MonoBehaviour {

    private bool paused = false;
    private bool coroutine = false;
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
        if (player.transform.position.x < -9.0f || player.transform.position.y < -5.0f)
        {
            if (!coroutine)
            {
                StartCoroutine(gameOver());
            }
        }
    }
    public void SetSuccess(bool p_successValue)
    {
        if(!p_successValue)
        {
            //game over logic
            if (!coroutine)
            {
                StartCoroutine(gameOver());
            }
        }
        else
        {
            paused = false;
            player.GetComponent<RunnerBehavior>().Paused = false;
            GetComponent<TerrainManager>().CompletedSimon();
            GetComponent<TerrainManager>().isPaused = false;
        }
    }
    public void SetPause()
    {
        paused = true;
        int simonCount = GetComponent<Simon>().SimonList.Count;
        GetComponent<Simon>().startSimon(1.5f * simonCount);
        player.GetComponent<RunnerBehavior>().Paused = true;
        GetComponent<TerrainManager>().isPaused = true;
    }
    IEnumerator gameOver()
    {
        coroutine = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(GameObject.FindGameObjectWithTag("UI"));
        Application.LoadLevel(2);
    }
}
