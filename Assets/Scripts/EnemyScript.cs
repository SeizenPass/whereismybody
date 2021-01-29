using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
                player.GetComponent<Transform>().position = GetComponent<Transform>().position;
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
