using System;
using UnityEngine;
using System.Collections;
using System.Text;
using Prime31;

public class RunnerBehavior : MonoBehaviour
{

    public float MaxJumpTime = 0.5f;
    public float jetpackPower = 350f;
    public float jumpPower = 350f;
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
        if (Paused)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.W) && Math.Abs(rigidBody.velocity.y) < 0.15f)
        {
            rigidBody.AddForce(new Vector2(0.0f, jumpPower), ForceMode2D.Impulse);
            jumping = true;
            jumpTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //attack code
            attacking = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //slide code
            sliding = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            sliding = false;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            jumping = false;
        }
        if (jumping)
        {

            jumpTime += Time.fixedDeltaTime;
            rigidBody.AddForce(new Vector2(0.0f, jetpackPower * (MaxJumpTime - jumpTime/2.0f) / MaxJumpTime), ForceMode2D.Force);

            if (jumpTime > MaxJumpTime)
            {
                jumping = false;
            }
        }
        
        rigidBody.AddForce(new Vector2(0.0f, -Gravity), ForceMode2D.Force);
        
        if (sliding)
        {
            //sliding logic
        }
        if (attacking)
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
        
    }
	// Update is called once per frame
	void FixedUpdate () {

	      
        
        
        
    }
}
