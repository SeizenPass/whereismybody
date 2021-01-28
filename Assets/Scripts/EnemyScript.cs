using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyScript : MonoBehaviour
{
    GroundDetector groundDetector;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        groundDetector = GetComponentInChildren<GroundDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player == collision.gameObject)
        {
            if (player.GetComponent<PlayerInput>().enabled == true)
            {
                print("Collision");
                GetComponent<PlayerInput>().enabled = true;
                GetComponent<PlayerScript>().enabled = true;
                player.GetComponent<PlayerInput>().enabled = false;
            }
            else
            {
                GetComponent<PlayerInput>().enabled = false;
                GetComponent<PlayerScript>().enabled = false;
                player.GetComponent<PlayerInput>().enabled = true;
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
