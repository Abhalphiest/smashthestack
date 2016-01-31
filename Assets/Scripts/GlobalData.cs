using UnityEngine;
using System.Collections;

// maintains data between scenes
public class GlobalData : MonoBehaviour {

    public int numberOfRestarts = 0;
    public bool shortStart = false;

	// Use this for initialization
	void Start () {

        // THERE CAN ONLY BE ONE!
        if (FindObjectsOfType<GlobalData>() != null)
        {
            if (FindObjectsOfType<GlobalData>().Length > 1) {
                Destroy(transform.gameObject);
            }
        }

        // accross frames
        DontDestroyOnLoad(transform.gameObject);

        // Terrain Manager checks to see if shortstart is true
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RecordRestart()
    {
        print("restart recorded");
        numberOfRestarts++;
        shortStart = true;
    }
}
