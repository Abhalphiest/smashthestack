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
    private bool jumping = false;
    private bool sliding = false;
    private bool attacking = false;
    private float jumpTime = 0;

    private Rigidbody2D rigidBody;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
    }
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            jumping = true;
            jumpTime = 0;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            //attack code
            attacking = true;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            //slide code
            sliding = true;
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            sliding = false;
        }
    }
	// Update is called once per frame
	void FixedUpdate () {

	    if (Paused)
	    {
	        return;
	    }        
        if(jumping)
        {
            
            jumpTime += Time.fixedDeltaTime;
            rigidBody.AddForce(new Vector2(0.0f, JumpPower),ForceMode2D.Force);
            
            if (jumpTime > MaxJumpTime || !Input.GetKey(KeyCode.W))
            {
                jumping = false;
            }
        }
        if(sliding)
        {
            //sliding logic
        }
        if(attacking)
        {
            //attackinglogic
        }
	    if (transform.position.x < BaseX)
	    {
            rigidBody.AddForce(new Vector2(XResetSpeed, 0.0f), ForceMode2D.Force);
	    }
        else
        {
            rigidBody.velocity = new Vector2(0.0f, rigidBody.velocity.y);
        }
        rigidBody.AddForce(new Vector2(0.0f, -Gravity), ForceMode2D.Force);
    }
}
