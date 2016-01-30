using UnityEngine;
using System.Collections;
using System.Text;

public class RunnerBehavior : MonoBehaviour
{

    public float MaxJumpTime = 0.5f;
    public float JumpPower = 6f;
    public float Gravity = 5f;

    private bool _jumping = false;
    private float jumpTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (gameObject.transform.position.y > 0)
	    {
	        gameObject.GetComponent<Rigidbody2D>().velocity -= Gravity * Vector2.up * Time.fixedDeltaTime;
	    }
	    else
	    {
	        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	        gameObject.transform.position = Vector3.zero;
	    }
	    if (Input.GetKeyDown(KeyCode.Space) && gameObject.transform.position.y <= 0.05f)
	    {
	        _jumping = true;
	        jumpTime = 0;
	    }
        if(_jumping)
        {
            jumpTime += Time.fixedDeltaTime;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up*JumpPower;
            if (jumpTime > MaxJumpTime || !Input.GetKey(KeyCode.Space))
            {
                _jumping = false;
            }
        }
    }
}
