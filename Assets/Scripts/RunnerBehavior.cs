using System;
using UnityEngine;
using System.Collections;
using System.Text;
using Prime31;

public class RunnerBehavior : MonoBehaviour
{

    public float MaxJumpTime = 0.5f;
    public float JumpPower = 350f;
    public float Gravity = 300f;
    public float BaseX = 0;
    public float XResetSpeed = 200f;
    public bool Paused;
    public float runSpeed = .2f;
    private bool _jumping = false;
    private float _jumpTime = 0;

    private Rigidbody2D rigidBody;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
    }
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            _jumping = true;
            _jumpTime = 0;
        }
    }
	// Update is called once per frame
	void FixedUpdate () {

	    if (Paused)
	    {
	        return;
	    }        
        if(_jumping)
        {
            
            _jumpTime += Time.fixedDeltaTime;
            rigidBody.AddForce(new Vector2(0.0f, JumpPower),ForceMode2D.Force);
            
            if (_jumpTime > MaxJumpTime || !Input.GetKey(KeyCode.Space))
            {
                _jumping = false;
            }
        }

	    if (transform.position.x < BaseX)
	    {
            rigidBody.AddForce(new Vector2(XResetSpeed, 0.0f), ForceMode2D.Force);
	    }
        else
	    {
	        
	    }

        rigidBody.AddForce(new Vector2(0.0f, -Gravity), ForceMode2D.Force);
    }
}
