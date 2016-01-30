using System;
using UnityEngine;
using System.Collections;
using System.Text;
using Prime31;
using Random = UnityEngine.Random;

public class RunnerBehavior : MonoBehaviour
{

    public float MaxJumpTime = 0.5f;
    public float MaxSlideTime = 0.5f;
    public float JumpPower = 350f;
    public float Gravity = 300f;
    public float BaseX = 1.37f;
    public float XResetSpeed = 0.5f;
    public bool Paused;

    private bool _jumping = false;
    private float _jumpTime = 0;
    private Vector2 _velocity = Vector3.zero;
    private bool _sliding;
    private float _slideTime;

    private SpriteRenderer _graphic;


    private CharacterController2D _characterController2D;

    // Use this for initialization
    void Start()
    {
        _characterController2D = GetComponent<CharacterController2D>();
        _graphic = transform.FindChild("PlaceholderRunner").gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
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

        if (_sliding)
        {
            _slideTime += Time.deltaTime;
            if (_slideTime > MaxSlideTime || _characterController2D.collisionState.right)
            {
                _sliding = false;
                var colliderSize = GetComponent<BoxCollider2D>().size;
                colliderSize.Set(colliderSize.y, colliderSize.x);
                GetComponent<BoxCollider2D>().size = colliderSize;
                _characterController2D.recalculateDistanceBetweenRays();
                //transform.Translate((colliderSize.x - colliderSize.y) * Vector3.up / 2);
                transform.Translate(Mathf.Abs(colliderSize.x - colliderSize.y) * Vector3.up * 4);
                //TODO: Change animation
                transform.FindChild("PlaceholderRunner").Rotate(0, 0, -90);
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && _characterController2D.isGrounded)
        {
            _slideTime = 0;
            _sliding = true;
            var colliderSize = GetComponent<BoxCollider2D>().size;
            colliderSize.Set(colliderSize.y, colliderSize.x);
            GetComponent<BoxCollider2D>().size = colliderSize;
            _characterController2D.recalculateDistanceBetweenRays();
            //TODO: Change animation
            transform.FindChild("PlaceholderRunner").Rotate(0,0,90);
        }

        if (Input.GetKeyDown(KeyCode.W) && _characterController2D.isGrounded && !_sliding)
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

        if (!Input.GetKey(KeyCode.W))
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

        _characterController2D.move(_velocity * Time.deltaTime - Vector2.left * 0.5f);

        if (transform.position.y < -25)
        {
            transform.position += 30 * Vector3.up;
            transform.position += transform.position.x*Vector3.left;
        }

        if (!_jumping && !_sliding && _characterController2D.isGrounded && IsSkating())
        {
            GetComponentInChildren<ParticleSystem>().Emit(Random.Range(0, 10));
        }
    }

    private bool IsSkating()
    {
        return _graphic.sprite.name.EndsWith("1") || _graphic.sprite.name.EndsWith("6");
    }
}