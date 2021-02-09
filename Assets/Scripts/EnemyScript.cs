using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] public float movementVelocity;
    [SerializeField] public float jumpVelocity;
    
    GroundDetector groundDetector;
    private Animator _animator;
    private GameObject player;
    private Rigidbody2D body;
    Vector2 direction;
    private bool isPossessed = false;
    public bool facingRight = true;
    
    
    private int _animatorSpeedName;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animatorSpeedName = Animator.StringToHash("Speed");
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
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
        
        _animator.SetFloat(_animatorSpeedName, Mathf.Abs(velocity.x));
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
