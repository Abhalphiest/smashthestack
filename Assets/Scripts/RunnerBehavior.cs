using UnityEngine;
using System.Collections;
using System.Text;
using Prime31;

public class RunnerBehavior : MonoBehaviour
{

    public float MaxJumpTime = 0.5f;
    public float JumpPower = 6f;
    public float Gravity = 5f;
    public float BaseX = 0;
    public float XResetSpeed = 0.5f;

    private bool _jumping = false;
    private float _jumpTime = 0;
    private Vector2 _velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.position += Vector3.left * Time.fixedDeltaTime;

        if (GetComponent<CharacterController2D>().isGrounded)
	    {
            _velocity = Vector2.zero;
	    }
        _velocity -= Gravity * Vector2.up * Time.fixedDeltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && GetComponent<CharacterController2D>().isGrounded)
	    {
	        _jumping = true;
	        _jumpTime = 0;
	    }
        if(_jumping)
        {
            _jumpTime += Time.fixedDeltaTime;
            _velocity = Vector2.up*JumpPower;
            if (_jumpTime > MaxJumpTime || !Input.GetKey(KeyCode.Space))
            {
                _jumping = false;
            }
        }

	    if (transform.position.x < BaseX)
	    {
	        _velocity.x = XResetSpeed;
	    }
        else
	    {
	        _velocity.x = 0;
	    }

        GetComponent<CharacterController2D>().move(_velocity*Time.fixedDeltaTime - Vector2.left * Time.fixedDeltaTime);
    }
}
