using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    GroundDetector groundDetector;

    private GameObject player;
    private PossessScript ps;
    private RigidbodyConstraints2D _constraints2D;

    private bool isPossessed = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        groundDetector = GetComponentInChildren<GroundDetector>();
        ps = GetComponentInChildren<PossessScript>();
        _constraints2D = GetComponent<Rigidbody2D>().constraints;
    }

    // Update is called once per frame

    private void OnEnable()
    {
        GetComponent<PlayerScript>().facingRight = player.GetComponent<PlayerScript>();
    }

    private void OnPossess()
    {
        if (groundDetector.IsAtGround)
        {
            if (!isPossessed && ps.playerAround)
            {
                isPossessed = true;
                GetComponent<PlayerScript>().enabled = true;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                player.GetComponentInChildren<SpriteRenderer>().enabled = false;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                player.GetComponent<CapsuleCollider2D>().enabled = false;
                player.GetComponent<PlayerScript>().enabled = false;
            }
            else if (isPossessed)
            {
                isPossessed = false;
                GetComponent<PlayerScript>().enabled = false;
                GetComponent<Rigidbody2D>().constraints = _constraints2D;
                player.GetComponentInChildren<SpriteRenderer>().enabled = true;
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

    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        var dst = destination.GetComponent(type) as T;
        if (!dst) dst = destination.AddComponent(type) as T;
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            if (field.IsStatic) continue;
            field.SetValue(dst, field.GetValue(original));
        }
        var props = type.GetProperties();
        foreach (var prop in props)
        {
            if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
            prop.SetValue(dst, prop.GetValue(original, null), null);
        }
        return dst as T;
    }
}
