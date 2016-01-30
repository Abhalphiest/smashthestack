using UnityEngine;
using System.Collections;

public class Hopper : MonoBehaviour {

    Rigidbody2D thisRigidBody;
    public float hopFrequency;
    public float hopForce;
    private float count = 0.0f;

	// Use this for initialization
	void Start () {
        this.thisRigidBody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        count += 0.01f;
        if(count > hopFrequency){        // Add the force
         thisRigidBody.AddForce(new Vector2(0, hopForce));
         count = 0;
        }
    }
	
}
