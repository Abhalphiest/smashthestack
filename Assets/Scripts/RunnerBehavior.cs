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
    public float BaseX = 1.37f;
    public float XResetSpeed = 0.5f;
    public bool Paused;

    private bool _jumping = false;
    private float _jumpTime = 0;
    private Vector2 _velocity = Vector3.zero;
    private bool _jumpKeyDown;
    private bool _jumpKey;

    private CharacterController2D _characterController2D;

    // Use this for initialization
    void Start()
    {
        _characterController2D = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        _jumpKeyDown = Input.GetKeyDown(KeyCode.W);
        _jumpKey = Input.GetKey(KeyCode.W);

        if (Paused)
        {
            return;
        }

        gameObject.transform.position += Vector3.left * 0.5f;

        if (_characterController2D.isGrounded)
        {
            _velocity = Vector2.zero;
        }
        _velocity -= Gravity * Vector2.up * Time.deltaTime;

        if (_jumpKeyDown && _characterController2D.isGrounded)
        {
            _jumping = true;
            _jumpTime = 0;
        }
        if (_jumping)
        {
            _jumpTime += Time.fixedDeltaTime;
            _velocity = Vector2.up * JumpPower;
            if (_jumpTime > MaxJumpTime)
            {
                _jumping = false;
            }
        }

        if (_characterController2D.collisionState.above)
        {
            _jumping = false;
            _velocity.y = Mathf.Min(_velocity.y, 0);
        }

        if (!_jumpKey)
        {
            _jumping = false;
            _velocity.y = Mathf.Min(_velocity.y, Mathf.Abs(_velocity.y / 2));
        }

        if (transform.position.x < BaseX && _characterController2D.isGrounded)
        {
            _velocity.x = XResetSpeed;
        }
        else
        {
            _velocity.x = 0;
        }

        GetComponent<CharacterController2D>().move(_velocity * Time.deltaTime - Vector2.left * 0.5f);

        if (transform.position.y < -25)
        {
            transform.position += 30 * Vector3.up;
        }
    }
}

/*using System;
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
}*/
