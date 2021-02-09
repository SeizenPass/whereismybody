using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] public float movementVelocity;
    [SerializeField] public float jumpVelocity;

    private Animator _animator;
    public bool facingRight = true;
    GroundDetector groundDetector;
    Rigidbody2D body; 
    Vector2 direction;
    private CapsuleCollider2D _capsuleCollider2D;

    private int _animatorSpeedName;
    private int _animatorUpName;
    private int _animatorOnGroundName;
    
    // Start is called before the first frame update
    void Start()
    {
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _animatorSpeedName = Animator.StringToHash("Speed");
        _animatorOnGroundName = Animator.StringToHash("OnGround");
        _animatorUpName = Animator.StringToHash("Up");
        Debug.Log("Started " + gameObject.name);
        _animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        groundDetector = GetComponentInChildren<GroundDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 velocity = body.velocity;
        velocity.x = direction.x * movementVelocity;       
        
        if (direction.y > 0.0f && groundDetector.IsAtGround) {
            velocity.y = jumpVelocity;
        }

        try
        {
            _animator.SetFloat(_animatorSpeedName, Mathf.Abs(velocity.x));
            _animator.SetFloat(_animatorUpName, velocity.y);
            _animator.SetBool(_animatorOnGroundName, groundDetector.IsAtGround);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        body.velocity = velocity;
    }

    void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
        if (enabled)
        {
            if (direction.x > 0.0f)
            {
                facingRight = true;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (direction.x < 0.0f)
            {
                facingRight = false;
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
    
    
}
