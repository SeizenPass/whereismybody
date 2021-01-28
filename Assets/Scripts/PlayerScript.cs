using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] public float movementVelocity;
    [SerializeField] public float jumpVelocity;

    GroundDetector groundDetector;
    Rigidbody2D body;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started " + gameObject.name);
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

        body.velocity = velocity;
    }

    void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
        Debug.Log("wahaha " + gameObject.name);
    }
}
