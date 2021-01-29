using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessEnemyScript : MonoBehaviour
{
    
    GroundDetector groundDetector;
    private GameObject player;
    private PossessAreaScript ps;
    private RigidbodyConstraints2D _constraints2D;
    Vector2 direction;
    private bool isPossessed = false;
    public bool facingRight = true;
    
    
    private int _animatorSpeedName;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        groundDetector = GetComponentInChildren<GroundDetector>();
        ps = GetComponentInChildren<PossessAreaScript>();
        _constraints2D = GetComponent<Rigidbody2D>().constraints;
    }
    
    private void OnPossess()
    {
        if (groundDetector.IsAtGround)
        {
            if (!isPossessed && ps.playerAround)
            {
                isPossessed = true;
                GetComponent<EnemyScript>().enabled = true;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                player.GetComponent<SpriteRenderer>().enabled = false;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                player.GetComponent<CapsuleCollider2D>().enabled = false;
                player.GetComponent<PlayerScript>().enabled = false;
            }
            else if (isPossessed)
            {
                isPossessed = false;
                GetComponent<EnemyScript>().enabled = false;
                GetComponent<Rigidbody2D>().constraints = _constraints2D;
                player.GetComponent<SpriteRenderer>().enabled = true;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                player.GetComponent<CapsuleCollider2D>().enabled = true;
                player.GetComponent<PlayerScript>().enabled = true;
                Vector3 pos = GetComponent<Transform>().position;
                if (GetComponent<PlayerScript>().facingRight)
                {
                    pos.x += GetComponent<Transform>().lossyScale.x / 2;
                }
                else
                {
                    pos.x -= GetComponent<Transform>().lossyScale.x / 2;
                }
                player.GetComponent<Transform>().position = pos;
            }
        }
    }
}
