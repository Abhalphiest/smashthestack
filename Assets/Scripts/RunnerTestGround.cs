using UnityEngine;
using System.Collections;

public class RunnerTestGround : MonoBehaviour
{
    private float startX;

	// Use this for initialization
	void Start ()
	{
	    startX = transform.position.x;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    gameObject.transform.position += Vector3.left*Time.fixedDeltaTime;
	    if (gameObject.transform.position.x < startX-15)
	    {
            gameObject.transform.position -= Vector3.left * 30;
        }
    }
}
